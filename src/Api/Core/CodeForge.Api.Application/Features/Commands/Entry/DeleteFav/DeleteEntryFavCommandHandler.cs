using CodeForge.Common;
using CodeForge.Common.Events.Entry;
using CodeForge.Common.Infrastructure;
using MediatR;

namespace CodeForge.Api.Application.Features.Commands.Entry.CreateFav;

public class DeleteEntryFavCommandHandler : IRequestHandler<DeleteEntryFavCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryFavCommand request, CancellationToken cancellationToken)
    {
        DeleteEntryFavEvent @event = new() { EntryId = request.Id, CreatedBy = request.CreatedBy };

        QueueFactory.SendMessage(exchangeName: AppConstants.FAV_EXCHANGE_NAME,
                                 exchangeType: AppConstants.DEFAULT_EXCHANGE_TYPE,
                                 queueName: AppConstants.DELETE_ENTRY_FAV_QUEUE_NAME,
                                 obj: @event);

        return await Task.FromResult(true);

    }
}
