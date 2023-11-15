using CodeForge.Api.Domain.Models.Abstracts;

namespace CodeForge.Api.Domain.Models;

public class Entry : Entity
{
    public string Subject { get; set; }
    public string Content { get; set; }
    
    public virtual ICollection<EntryComment> EntryComments { get; set; }
    public virtual ICollection<EntryVote> EntryVotes { get; set; }
    public virtual ICollection<EntryFavorite> EntryFavorites { get; set; }

    public Guid OwnerId { get; set; }
    public virtual User Owner { get; set; }
}
