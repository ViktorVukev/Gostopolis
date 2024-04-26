namespace Gostopolis.Admin.Models.Identity;

using Gostopolis.Models;

public class UserInputModel : IMapFrom<LoginFormModel>
{
    public string Email { get; set; }

    public string Password { get; set; }
}