namespace Gostopolis.Data;

/// <summary>
/// Current status of user's application.
/// </summary>
public enum ApplicationStatus
{
    /// <summary>
    /// If an error occurred and a status has not been set to the application.
    /// </summary>
    None = 0,
    /// <summary>
    /// Value that is set by default to an application.
    /// </summary>
    Pending = 1,
    /// <summary>
    /// When the moderator approves the application.
    /// </summary>
    Approved = 2,
    /// <summary>
    /// When the moderator declines the application.
    /// </summary>
    Declined = 3
}