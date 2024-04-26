namespace Gostopolis.Identity.Models.Identity;

public class ChangePasswordInputModel
{
    public string UserId { get; set; }

    public string Token { get; set; }

    public string NewPassword { get; set; }
}