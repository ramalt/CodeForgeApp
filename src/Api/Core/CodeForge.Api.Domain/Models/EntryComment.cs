using CodeForge.Api.Domain.Models.Abstracts;

namespace CodeForge.Api.Domain.Models;

public class EntryComment : Entity
{
    public string Content { get; set; }

    public virtual ICollection<EntryCommentVote> EntryCommentVotes { get; set; }
    public virtual ICollection<EntryCommentFavorite> EntryCommentFavorites { get; set; }

    public Guid OwnerId { get; set; }
    public virtual User Owner { get; set; }
    public Guid EntryId { get; set; }
    public virtual Entry Entry { get; set; }
}
