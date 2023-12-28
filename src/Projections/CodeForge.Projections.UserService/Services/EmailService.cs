using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeForge.Projections.UserService.Services;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenereateConfirmationLink(Guid confirmationId) => _configuration["EmailConfirmationAddress"] + confirmationId;

    public async Task SendConfirmationEmail(string toMailAddress, string content) => Console.WriteLine($"Confirmation Mail: {content}");

}
