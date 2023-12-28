using MediatR;

namespace CodeForge.Api.Application.Features.Commands.User.EmailConfirm;

public class EmailConfirmCommand : IRequest<bool>
{
    public Guid ConfirmationId { get; set; }
}
