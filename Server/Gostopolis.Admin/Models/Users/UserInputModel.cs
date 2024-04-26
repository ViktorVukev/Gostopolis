namespace Gostopolis.Admin.Models.Users;

using System.ComponentModel.DataAnnotations;
using static Data.DataConstants.Common;

public class UserInputModel()
{
    [MinLength(MinNameLength)]
    [MaxLength(MaxNameLength)]
    public string? FirstName { get; set; }

    [MinLength(MinNameLength)]
    [MaxLength(MaxNameLength)]
    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; } = Guid.NewGuid().ToString();
}