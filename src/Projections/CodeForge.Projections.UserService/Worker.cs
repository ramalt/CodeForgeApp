using CodeForge.Common;
using CodeForge.Common.Events.User;
using CodeForge.Common.Infrastructure;
using CodeForge.Projections.UserService.Services;

namespace CodeForge.Projections.UserService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly Services.UserService _userService;
    private readonly EmailService _emailService;

    public Worker(ILogger<Worker> logger, EmailService emailService, Services.UserService userService)
    {
        _logger = logger;
        _emailService = emailService;
        _userService = userService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        QueueFactory.CreateBasicConsumer()
            .EnsureExchange(AppConstants.USER_EXCHANGE_NAME)
            .EnsureQueue(AppConstants.USER_EMAIL_CHANGED_QUEUE_NAME, AppConstants.USER_EXCHANGE_NAME)
            .Receive<UserEmailChangedEvent>(async user =>
            {
                
                var confirmationId = await _userService.CreateEmailConfirmation(user);

                // Generate Link
                var link = _emailService.GenereateConfirmationLink(confirmationId);

                // Send Email
                await _emailService.SendConfirmationEmail(user.NewEmail, link);
            })
            .startConsuming(AppConstants.USER_EMAIL_CHANGED_QUEUE_NAME);
    }
}
