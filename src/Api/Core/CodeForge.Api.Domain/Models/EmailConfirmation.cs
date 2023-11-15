using CodeForge.Api.Domain.Models.Abstracts;

namespace CodeForge.Api.Domain.Models;

public class EmailConfirmation : Entity
{
    public string OldEmailAddress { get; set; }
    public string NewEmailAddress { get; set; }
}
