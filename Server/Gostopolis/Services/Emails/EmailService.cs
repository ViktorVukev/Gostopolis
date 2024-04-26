namespace Gostopolis.Services.Emails;

using SendGrid;
using SendGrid.Helpers.Mail;
using static Constants.SendGrid;

/// <summary>
/// Service that allows the app to send emails from our business email.
/// </summary>
public class EmailService : IEmailService
{
    /// <summary>
    /// Sending an email functionality
    /// </summary>
    /// <param name="subject">The subject of the email</param>
    /// <param name="message">The body of the email</param>
    /// <param name="toEmail">Email which would receive this email</param>
    /// <returns>Whether the email is sent or not</returns>
    public async Task<bool> Send(string subject, string message, string toEmail)
    {
        // Loading SendGrid client
        var apiKey = Environment.GetEnvironmentVariable(ApiKey);
        var client = new SendGridClient(apiKey);

        // Building the email
        var msg = new SendGridMessage
        {
            From = new EmailAddress(SenderEmail, SenderName),
            Subject = subject,
            PlainTextContent = message
        };
        msg.AddTo(new EmailAddress(toEmail));

        // Sending the email
        var response = await client.SendEmailAsync(msg);
        return response.IsSuccessStatusCode;
    }
}
