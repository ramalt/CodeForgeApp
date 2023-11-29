using AutoMapper;
using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Common;
using CodeForge.Common.Events.User;
using CodeForge.Common.Infrastructure;
using CodeForge.Common.Infrastructure.Exceptions;
using CodeForge.Common.Infrastructure.Helpers;
using CodeForge.Common.ViewModels.RequestModels;
using MediatR;

using Models = CodeForge.Api.Domain.Models;

namespace CodeForge.Api.Application.Features.Commands.User.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _manager;

    public CreateUserCommandHandler(IRepositoryManager manager, IMapper mapper)
    {
        _manager = manager;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existUser = await _manager.User.GetSingleAsync(u => u.Email == request.Email);

        if (existUser is not null)
            throw new DbValidationException("User already exist");



        var dbUser = _mapper.Map<Models.User>(request);

        dbUser.Password = PasswordEncrypter.Encrypt(request.Password);

        await _manager.User.AddAsync(dbUser);

        var result = await _manager.SaveAsync();

        if (result > 0)
        {
            UserEmailChangedEvent @event = new()
            {
                OldEmail = null,
                NewEmail = dbUser.Email
            };

            QueueFactory.SendMessage(exchangeName: AppConstants.USER_EXCHANGE_NAME,
                                     exchangeType: AppConstants.DEFAULT_EXCHANGE_TYPE,
                                     queueName: AppConstants.USER_EMAIL_CHANGED_QUEUE_NAME,
                                     obj: @event);
        }

        return dbUser.Id;


    }
}
