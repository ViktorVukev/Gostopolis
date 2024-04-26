namespace Gostopolis.Identity.Models.Identity;

using System.ComponentModel.DataAnnotations;
using static Gostopolis.Data.DataConstants.Common;
using static Data.DataConstants.Identity;

public class UserInputModel(
    string firstName, 
    string lastName, 
    string phoneNumber, 
    string email, 
    string password)
{
    [MinLength(MinNameLength)]
    [MaxLength(MaxNameLength)]
    public string? FirstName { get; set; } = firstName;

    [MinLength(MinNameLength)]
    [MaxLength(MaxNameLength)]
    public string? LastName { get; set; } = lastName;

    [RegularExpression(PhoneNumberRegularExpression)]
    public string? PhoneNumber { get; set; } = phoneNumber;

    [EmailAddress]
    [Required]
    [MinLength(MinEmailLength)]
    [MaxLength(MaxEmailLength)]
    public string Email { get; set; } = email;

    [Required]
    public string Password { get; set; } = password;
}