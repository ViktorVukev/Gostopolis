namespace Gostopolis.Identity.Data.Models;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public bool LoginNotification { get; set; } = true;

    public string ImageUrl { get; set; } = "https://res.cloudinary.com/dduhwzpfw/image/upload/v1672849735/avatar.png";

    public ICollection<Application> Applications { get; set; } = new List<Application>();
}