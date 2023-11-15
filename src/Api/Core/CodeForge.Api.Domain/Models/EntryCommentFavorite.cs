using CodeForge.Api.Domain.Models.Abstracts;

namespace CodeForge.Api.Domain.Models;

public class EntryCommentFavorite : Entity
{
    public Guid EntryCommentId { get; set; }
    public virtual EntryComment EntryComment { get; set; }
    
    public Guid OwnerId { get; set; }
    public virtual User Owner { get; set; }
}
