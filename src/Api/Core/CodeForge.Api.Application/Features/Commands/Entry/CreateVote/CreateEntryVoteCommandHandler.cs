using CodeForge.Common;
using CodeForge.Common.Events.Entry;
using CodeForge.Common.Infrastructure;
using CodeForge.Common.ViewModels.RequestModels;
using MediatR;

namespace CodeForge.Api.Application.Features.Commands.Entry.CreateVote;

public class CreateEntryVoteCommandHandler : IRequestHandler<CreateEntryVoteCommand, bool>
{
    public async Task<bool> Handle(CreateEntryVoteCommand request, CancellationToken cancellationToken)
    {
        CreateEntryVoteEvent @event = new() { EntryId = request.EntryId, CreatedBy = request.CreatedBy, Vote = request.Vote };

        QueueFactory.SendMessage(exchangeName: AppConstants.VOTE_EXCHANGE_NAME,
                         exchangeType: AppConstants.DEFAULT_EXCHANGE_TYPE,
                         queueName: AppConstants.CREATE_ENTRY_VOTE_QUEUE_NAME,
                         obj: @event);

        return await Task.FromResult(true);
    }
}
