using CodeForge.Api.Domain.Models.Abstracts;

namespace CodeForge.Api.Domain.Models;

public class EntryCommentFavorite : Entity
{
    public Guid EntryId { get; set; }
    public virtual EntryComment Entry { get; set; }
    
    public Guid OwnerId { get; set; }
    public virtual User Owner { get; set; }
}
