using AutoMapper;
using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Common;
using CodeForge.Common.Events.User;
using CodeForge.Common.Infrastructure;
using CodeForge.Common.Infrastructure.Exceptions;
using CodeForge.Common.ViewModels.RequestModels;
using MediatR;



namespace CodeForge.Api.Application.Features.Commands.User.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _manager;

    public UpdateUserCommandHandler(IRepositoryManager manager, IMapper mapper)
    {
        _manager = manager;
        _mapper = mapper;
    }
    public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var dbUser = await _manager.User.GetByIdAsync(request.Id);

        if (dbUser is null)
            throw new DbValidationException("User not found");

        _mapper.Map(request, dbUser);

        await _manager.User.UpdateAsync(dbUser);

        var result = await _manager.SaveAsync();

        var emailChanged = string.CompareOrdinal(dbUser.Email, request.Email) != 0;

        if (result > 0 && emailChanged)
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

            dbUser.EmailConfirmed = false;

            await _manager.User.UpdateAsync(dbUser);
            await _manager.SaveAsync();
        }
        return dbUser.Id;
    }
}
