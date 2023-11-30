using CodeForge.Common;
using CodeForge.Common.Events.EntryComment;
using CodeForge.Common.Infrastructure;
using MediatR;

namespace CodeForge.Api.Application.Features.Commands.EntryComment.DeleteFav;

public class DeleteEntryCommentFavCommandHandler : IRequestHandler<DeleteEntryCommentFavCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryCommentFavCommand request, CancellationToken cancellationToken)
    {
        DeleteEntryCommentFavEvent @event = new() { Id = request.Id, CreatedBy = request.CreatedBy };

        QueueFactory.SendMessage(exchangeName: AppConstants.FAV_EXCHANGE_NAME,
                 exchangeType: AppConstants.DEFAULT_EXCHANGE_TYPE,
                 queueName: AppConstants.DELETE_COMMENT_FAV_QUEUE_NAME,
                 obj: @event);

        return await Task.FromResult(true);
    }
}
