using SmtpGmailClient.Api.Model;

namespace SmtpGmailClient.Api.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(Email email);
        Task SendErrorEmailAsync(Exception message);
    }
}
