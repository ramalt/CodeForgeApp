namespace CodeForge.Common.Events.User;

public class UserEmailChangedEvent
{
    public string OldEmail { get; set; }
    public string NewEmail { get; set; }
}
