using CodeForge.Api.Domain.Models.Abstracts;
using CodeForge.Common.ViewModels;

namespace CodeForge.Api.Domain.Models;

public class EntryCommentVote : Entity
{
    public VoteType VoteType { get; set; }
    
    public Guid EntryCommentId { get; set; }
    public virtual EntryComment EntryComment { get; set; }
    public Guid OwnerId { get; set; }
    public virtual User Owner { get; set; }
}


