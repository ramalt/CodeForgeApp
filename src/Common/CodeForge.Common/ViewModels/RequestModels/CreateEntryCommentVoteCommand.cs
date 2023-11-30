using MediatR;

namespace CodeForge.Common.ViewModels.RequestModels;

public class CreateEntryCommentVoteCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public VoteType Vote { get; set; }
    public Guid CreatedBy { get; set; }

    public CreateEntryCommentVoteCommand(Guid createdBy, VoteType vote, Guid id)
    {
        CreatedBy = createdBy;
        Vote = vote;
        Id = id;
    }

    public CreateEntryCommentVoteCommand()
    {
        
    }
}
