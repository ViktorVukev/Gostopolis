namespace Gostopolis.Identity.Models.Identity;

public class UserOutputModel
{
    public UserOutputModel()
    {
        
    }

    public UserOutputModel(string userId)
    {
        this.UserId = userId;
    }

    public UserOutputModel(string token, string userId)
        : this(userId)
    {
        this.Token = token;
    }

    public string? Token { get; set; }

    public string UserId { get; set; }
}