using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;

using SmtpGmailClient.Api.Model;


namespace SmtpGmailClient.Api.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(Email req)
        {
            using SmtpClient smtp = new SmtpClient();
            try
            {
                MimeMessage email = new MimeMessage
                {
                    From = { MailboxAddress.Parse(_configuration.GetSection("EmailUsername").Value) }
                };
                foreach (string emailAddress in req.EmailAddress)
                {
                    email.To.Add(MailboxAddress.Parse(emailAddress));
                }

                email.Subject = req.EmailSubject;
                email.Body = new TextPart(TextFormat.Plain)
                {
                    Text = req.EmailBody
                };
                smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_configuration.GetSection("EmailUsername").Value, _configuration.GetSection("EmailPassword").Value);
                await smtp.SendAsync(email);
            }
            catch
            {
                throw;
            }
            //finally
            //{
            //    smtp.Dispose();
            //}
        }
        public async Task SendErrorEmailAsync(Exception Message)
        {
            var email = new Email
            {
                EmailSubject = "This is Error Email from AEL!",
                EmailBody = "Hello From AEL" +
                            "- Project Name: SmtpGmail " +
                            Message +
                            $"- Time: {DateTime.Now}\n\n" +
                            $"- StackTrace: {Message.StackTrace}"
            };

            email.EmailAddress.AddRange(new[]
            {
                "" //Add Multiple Receipent to Send Bulk Mail
        
    });


            await SendEmailAsync(email);
        }
    }
}
