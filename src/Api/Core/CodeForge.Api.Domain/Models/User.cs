using CodeForge.Api.Domain.Models.Abstracts;

namespace CodeForge.Api.Domain.Models;

public class User : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool EmailConfirmed { get; set; }

    public virtual ICollection<Entry> Entries{ get; set; }
    public virtual ICollection<EntryComment> EntryComments{ get; set; }
    public virtual ICollection<EntryFavorite> EntryFavorites{ get; set; }
    public virtual ICollection<EntryCommentFavorite> EntryCommentFavorites{ get; set; }
}
