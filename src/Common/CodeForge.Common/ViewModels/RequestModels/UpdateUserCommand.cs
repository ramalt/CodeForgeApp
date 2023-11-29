using MediatR;

namespace CodeForge.Common.ViewModels.RequestModels;

public class UpdateUserCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
}
