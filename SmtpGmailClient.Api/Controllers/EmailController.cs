using Microsoft.AspNetCore.Mvc;
using SmtpGmailClient.Api.Model;
using SmtpGmailClient.Api.Services;

namespace SmtpGmailClient.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {

        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService) => _emailService = emailService;


        [HttpGet]
        public async Task<IActionResult> SendEmailToUser()
        {
            try
            {
                Email email = new()
                {
                    EmailBody = "This is a Demo Email Body",
                    EmailSubject = "Demo Email Message from AEL",
                    EmailAddress =
                [
                    "fshanto@ael-bd.com"
                ],


                };
                var sendEmail = _emailService.SendEmailAsync(email);
                return Ok(sendEmail);

            }
            catch (Exception ex) {
                await _emailService.SendErrorEmailAsync(ex);
                throw;
            
            }
            
        }
    }
}
