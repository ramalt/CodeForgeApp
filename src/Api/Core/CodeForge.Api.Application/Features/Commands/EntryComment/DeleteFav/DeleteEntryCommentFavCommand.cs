using MediatR;

namespace CodeForge.Api.Application.Features.Commands.EntryComment.DeleteFav;

public class DeleteEntryCommentFavCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public Guid CreatedBy { get; set; }

    public DeleteEntryCommentFavCommand(Guid createdBy, Guid id)
    {
        CreatedBy = createdBy;
        Id = id;
    }

    public DeleteEntryCommentFavCommand()
    {
        
    }
}
