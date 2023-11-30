using MediatR;

namespace CodeForge.Common.ViewModels.RequestModels;

public class DeleteEntryVoteCommand : IRequest<bool>
{
    public Guid EntryId { get; set; }
    public Guid CreatedBy { get; set; }
    public VoteType Vote { get; set; }
}
