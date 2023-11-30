using CodeForge.Common;
using CodeForge.Common.Events.EntryComment;
using CodeForge.Common.Infrastructure;
using CodeForge.Common.ViewModels.RequestModels;
using MediatR;

namespace CodeForge.Api.Application.Features.Commands.EntryComment.CreateVote;

public class CreateEntryCommentVoteCommandHandler : IRequestHandler<CreateEntryCommentVoteCommand, bool>
{

    public async Task<bool> Handle(CreateEntryCommentVoteCommand request, CancellationToken cancellationToken)
    {
        CreateEntryCommentVoteEvent @event = new() { Id = request.Id, Vote = request.Vote, CreatedBy = request.CreatedBy };

        QueueFactory.SendMessage(exchangeName: AppConstants.VOTE_EXCHANGE_NAME,
                         exchangeType: AppConstants.DEFAULT_EXCHANGE_TYPE,
                         queueName: AppConstants.CREATE_COMMENT_VOTE_QUEUE_NAME,
                         obj: @event);

        return await Task.FromResult(true);

    }
}
