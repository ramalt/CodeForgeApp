using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Common.Infrastructure.Exceptions;
using MediatR;

namespace CodeForge.Api.Application.Features.Commands.User.EmailConfirm;

public class EmailConfirmCommandHandler : IRequestHandler<EmailConfirmCommand, bool>
{
    private readonly IRepositoryManager _manager;

    public EmailConfirmCommandHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<bool> Handle(EmailConfirmCommand request, CancellationToken cancellationToken)
    {
        var confirmation = await _manager.EmailConfirmation.GetByIdAsync(request.ConfirmationId);

        if (confirmation is null)
            throw new DbValidationException("Confirmation not found");

        var dbUser = await _manager.User.GetSingleAsync(u => u.Email == confirmation.NewEmailAddress);

        if (dbUser is null)
            throw new DbValidationException($"User not found with '{confirmation.NewEmailAddress}' email");

        if (dbUser.EmailConfirmed)
            throw new DbValidationException($"'{confirmation.NewEmailAddress}' is already confirmed");

        dbUser.EmailConfirmed = true;

        await _manager.User.UpdateAsync(dbUser);
        await _manager.SaveAsync();

        return true;

    }
}
