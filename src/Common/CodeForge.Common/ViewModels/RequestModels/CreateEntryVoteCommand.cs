using MediatR;

namespace CodeForge.Common.ViewModels.RequestModels;

public class CreateEntryVoteCommand : IRequest<bool>
{
    public Guid EntryId { get; set; }
    public Guid CreatedBy { get; set; }
    public VoteType Vote { get; set; }
}
