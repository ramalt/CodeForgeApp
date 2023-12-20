using MediatR;

namespace CodeForge.Common.ViewModels.RequestModels;

public class CreateEntryVoteCommand(VoteType vote, Guid createdBy, Guid entryId) : IRequest<bool>
{
    public Guid EntryId { get; set; } = entryId;
    public Guid CreatedBy { get; set; } = createdBy;
    public VoteType Vote { get; set; } = vote;
}
