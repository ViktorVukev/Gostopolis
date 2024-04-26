namespace Gostopolis.Admin.Models.Applications;

using Data;
using Users;

public class ApplicationDetailsOutputModel
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public UserDetailsOutputModel User { get; set; }

    public ApplicationStatus? Status { get; set; }

    public int? StatusInt { get; set; }

    public string DocumentUrl { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ApprovedOn { get; set; }

    public DateTime? DeclinedOn { get; set; }
}