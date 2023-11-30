using CodeForge.Common;
using CodeForge.Common.Infrastructure;
using CodeForge.Common.ViewModels.RequestModels;
using MediatR;

namespace CodeForge.Api.Application.Features.Commands.Entry.CreateVote;

public class DeleteEntryVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryVoteCommand request, CancellationToken cancellationToken)
    {
        DeleteEntryVoteCommand @event = new() { EntryId = request.EntryId, CreatedBy = request.CreatedBy, Vote = request.Vote };

        QueueFactory.SendMessage(exchangeName: AppConstants.VOTE_EXCHANGE_NAME,
                         exchangeType: AppConstants.DEFAULT_EXCHANGE_TYPE,
                         queueName: AppConstants.DELETE_ENTRY_VOTE_QUEUE_NAME,
                         obj: @event);

        return await Task.FromResult(true);
    }
}
