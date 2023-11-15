using CodeForge.Api.Domain.Models.Abstracts;

namespace CodeForge.Api.Domain.Models;

public class EntryFavorite : Entity
{
    public Guid EntryId { get; set; }
    public virtual Entry Entry { get; set; }
    
    public Guid OwnerId { get; set; }
    public virtual User Owner { get; set; }
}
