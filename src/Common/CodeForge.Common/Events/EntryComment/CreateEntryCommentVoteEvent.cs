using CodeForge.Common.ViewModels;

namespace CodeForge.Common.Events.EntryComment;

public class CreateEntryCommentVoteEvent
{
    public Guid Id { get; set; }
    public VoteType Vote { get; set; }
    public Guid CreatedBy { get; set; }
}
