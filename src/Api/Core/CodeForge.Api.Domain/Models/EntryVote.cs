using CodeForge.Api.Domain.Models.Abstracts;
using CodeForge.Common.ViewModels;

namespace CodeForge.Api.Domain.Models;

public class EntryVote : Entity
{
    public VoteType VoteType { get; set; }
    
    public Guid EntryId { get; set; }
    public virtual Entry Entry { get; set; }
    public User OwnerId { get; set; }
    public virtual User Owner { get; set; }
}


