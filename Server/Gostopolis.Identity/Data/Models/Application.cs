namespace Gostopolis.Identity.Data.Models;

using Gostopolis.Data;

public class Application
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public User User { get; set; }

    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;

    public string DocumentUrl { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public DateTime? ApprovedOn { get; set; }

    public DateTime? DeclinedOn { get; set; }
}
