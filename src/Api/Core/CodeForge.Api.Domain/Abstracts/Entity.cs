namespace CodeForge.Api.Domain.Models.Abstracts;

public abstract class Entity
{
    public Guid Id { get; set; }   
    public DateTime CreatedDate { get; set; }
}
