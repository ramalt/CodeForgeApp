using MediatR;

namespace CodeForge.Common.ViewModels.RequestModels;

public class CreateEntryCommand : IRequest<Guid>
{
    public string Subject { get; set; }
    public string Content { get; set; }
    public Guid? OwnerId { get; set; }

    public CreateEntryCommand(string subject, Guid ownerId, string content)
    {
        Subject = subject;
        OwnerId = ownerId;
        Content = content;
    }

    public CreateEntryCommand()
    {
        
    }
}
