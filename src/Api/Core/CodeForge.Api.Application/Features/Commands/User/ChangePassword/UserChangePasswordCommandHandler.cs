using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Common.Infrastructure.Exceptions;
using CodeForge.Common.Infrastructure.Helpers;
using CodeForge.Common.ViewModels.RequestModels;
using MediatR;

namespace CodeForge.Api.Application.Features.Commands.User.ChangePassword;

public class UserChangePasswordCommandHandler : IRequestHandler<UserChangePasswordCommand, bool>
{
    private readonly IRepositoryManager _manager;

    public UserChangePasswordCommandHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<bool> Handle(UserChangePasswordCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(request.Id.Value.ToString(), nameof(request.Id));

        var dbUser = await _manager.User.GetByIdAsync(request.Id.Value);

        if (dbUser is null)
            throw new DbValidationException("User not found");

        bool correctPassword = string.Compare(dbUser.Password, PasswordEncrypter.Encrypt(request.OldPassword)) > 0;

        if (!correctPassword)
            throw new DbValidationException("Passwords do not match");

        dbUser.Password = PasswordEncrypter.Encrypt(request.NewPassword);

        await _manager.User.UpdateAsync(dbUser);
        await _manager.SaveAsync();

        return true;


    }
}
