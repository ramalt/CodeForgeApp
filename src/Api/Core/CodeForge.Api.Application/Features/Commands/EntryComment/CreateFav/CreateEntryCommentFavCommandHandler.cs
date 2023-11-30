using CodeForge.Common;
using CodeForge.Common.Events.EntryComment;
using CodeForge.Common.Infrastructure;
using MediatR;

namespace CodeForge.Api.Application.Features.Commands.EntryComment.CreateFav;

public class CreateEntryCommentFavCommandHandler : IRequestHandler<CreateEntryCommentFavCommand, bool>
{
    public async Task<bool> Handle(CreateEntryCommentFavCommand request, CancellationToken cancellationToken)
    {
        CreateEntryCommentFavEvent @event = new() { EntryCommentId = request.EntryCommentId, CreatedBy = request.UserId };

        QueueFactory.SendMessage(exchangeName: AppConstants.FAV_EXCHANGE_NAME,
                                 exchangeType: AppConstants.DEFAULT_EXCHANGE_TYPE,
                                 queueName: AppConstants.CREATE_COMMENT_FAV_QUEUE_NAME,
                                 obj: @event);

        return await Task.FromResult(true);


    }
}
