using CodeForge.Common;
using CodeForge.Common.Events.EntryComment;
using CodeForge.Common.Infrastructure;
using MediatR;

namespace CodeForge.Api.Application.Features.Commands.EntryComment.DeleteVote;

public class DeleteEntryCommentVoteCommandHandler : IRequestHandler<DeleteEntryCommentVoteCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryCommentVoteCommand request, CancellationToken cancellationToken)
    {
        DeleteEntryCommentVoteEvent @event = new() { Id = request.Id, Vote = request.Vote, CreatedBy = request.CreatedBy };

        QueueFactory.SendMessage(exchangeName: AppConstants.VOTE_EXCHANGE_NAME,
         exchangeType: AppConstants.DEFAULT_EXCHANGE_TYPE,
         queueName: AppConstants.DELETE_COMMENT_VOTE_QUEUE_NAME,
         obj: @event);

        return await Task.FromResult(true);
    }
}
