using CodeForge.Common.ViewModels.Queries;
using MediatR;
namespace CodeForge.Common.ViewModels.RequestModels;

public class LoginUserCommand : IRequest<LoginUserViewModel>
{
    public string Email { get; set; }
    public string Password { get; set; }

    public LoginUserCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public LoginUserCommand()
    {

    }



}
