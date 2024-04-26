namespace Gostopolis.Services.Emails;

/// <summary>
/// SendGrid Functionality visible to the client
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sending an email functionality
    /// </summary>
    /// <param name="subject">The subject of the email</param>
    /// <param name="message">The body of the email</param>
    /// <param name="toEmail">Email which would receive this email</param>
    /// <returns>Whether the email is sent or not</returns>
    Task<bool> Send(string subject, string message, string toEmail);
}
