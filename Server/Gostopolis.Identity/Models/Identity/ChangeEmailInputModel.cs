namespace Gostopolis.Identity.Models.Identity;

public class ChangeEmailInputModel
{
    public string UserId { get; set; }

    public string Token { get; set; }

    public string NewEmail { get; set; }
}
