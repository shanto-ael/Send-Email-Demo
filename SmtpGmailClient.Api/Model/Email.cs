namespace SmtpGmailClient.Api.Model
{
    public class Email
    {
        public string EmailSubject { get; set; } = string.Empty;


        public string EmailBody { get; set; } = string.Empty;


        public List<string> EmailAddress { get; set; } = [];
    }
}
