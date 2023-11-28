using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Common.Infrastructure.Exceptions;
using CodeForge.Common.Infrastructure.Helpers;
using CodeForge.Common.ViewModels.Queries;
using CodeForge.Common.ViewModels.RequestModels;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace CodeForge.Api.Application.Features.Commands.User;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
{
    private readonly IRepositoryManager _manager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;

    public LoginUserCommandHandler(IRepositoryManager manager, IMapper mapper, IConfiguration config)
    {
        _manager = manager;
        _mapper = mapper;
        _config = config;
    }

    public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var dbUser = await _manager.User.GetSingleAsync(u => u.Email == request.Email);

        if (dbUser is null)
            throw new DbValidationException("User not found");

        var encyptedPass = PasswordEncrypter.Encrypt(request.Password);

        if (dbUser.Password != encyptedPass)
            throw new DbValidationException("Wrong email or password");

        if (!dbUser.EmailConfirmed)
            throw new DbValidationException("Email address not confirmed yet");

        var result = _mapper.Map<LoginUserViewModel>(dbUser);

        Claim[] claims = [
            new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
            new Claim(ClaimTypes.Email, dbUser.Email),
            new Claim(ClaimTypes.Name, dbUser.UserName),
            new Claim(ClaimTypes.GivenName, dbUser.FirstName),
            new Claim(ClaimTypes.Surname, dbUser.LastName),
        ];

        result.SetToken(GenerateToken(claims));
        return result;

    }

    private string GenerateToken(Claim[] claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AuthConfig:Secret"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expireDate = DateTime.Now.AddDays(10);
        var token = new JwtSecurityToken(claims: claims, expires: expireDate, signingCredentials: credentials, notBefore: DateTime.Now);
        return new JwtSecurityTokenHandler().WriteToken(token);

    }
}
