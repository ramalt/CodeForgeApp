using MediatR;

namespace CodeForge.Common.ViewModels.RequestModels;

public class CreateEntryCommentCommand : IRequest<Guid>
{
    public string Content { get; set; }
    public Guid? OwnerId { get; set; }
    public Guid EntryId { get; set; }

    public CreateEntryCommentCommand(Guid entryId, Guid ownerId, string content)
    {
        EntryId = entryId;
        OwnerId = ownerId;
        Content = content;
    }

    public CreateEntryCommentCommand()
    {
        
    }
}
