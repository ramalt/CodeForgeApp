using CodeForge.Common.ViewModels;

namespace CodeForge.Common.Events.Entry;

public class DeleteEntryVoteEvent
{
    public Guid EntryId { get; set; }
    public Guid CreatedBy { get; set; }
    public VoteType Vote { get; set; }
}
