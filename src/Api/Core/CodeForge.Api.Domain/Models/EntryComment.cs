using CodeForge.Api.Domain.Models.Abstracts;

namespace CodeForge.Api.Domain.Models;

public class EntryComment : Entity
{
    public string Content { get; set; }

    public virtual ICollection<EntryCommentVote> EntryVotes { get; set; }
    public virtual ICollection<EntryCommentFavorite> EntryFavorites { get; set; }

    public Guid OwnerId { get; set; }
    public User Owner { get; set; }
    public Guid EntryId { get; set; }
    public Entry Entry { get; set; }
}
