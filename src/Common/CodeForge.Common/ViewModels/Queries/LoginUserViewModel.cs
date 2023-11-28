using System.Dynamic;

namespace CodeForge.Common.ViewModels.Queries;

public class LoginUserViewModel
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string UserName { get; private set; }
    public string Token { get; private set; }

    public LoginUserViewModel(string token, string userName, string lastName, string firstName, Guid id)
    {
        Token = token;
        UserName = userName;
        LastName = lastName;
        FirstName = firstName;
        Id = id;
    }
    public LoginUserViewModel()
    {
        
    }

    public  void SetToken(string token) => Token = token;
}
