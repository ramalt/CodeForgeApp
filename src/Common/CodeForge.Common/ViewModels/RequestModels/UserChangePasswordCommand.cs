using MediatR;

namespace CodeForge.Common.ViewModels.RequestModels;

public class UserChangePasswordCommand : IRequest<bool>
{
    public Guid? Id { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }

    public UserChangePasswordCommand(string newPassword, string oldPassword, Guid? id)
    {
        NewPassword = newPassword;
        OldPassword = oldPassword;
        Id = id;
    }
}
