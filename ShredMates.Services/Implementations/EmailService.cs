using ShredMates.Services.Interfaces;
using ShredMates.Services.Models;

namespace ShredMates.Services.Implementations
{
    public class EmailService //: IEmailService
    {
        private readonly IEmailConfiguration emailConfig;

        public EmailService(IEmailConfiguration emailConfiguration)
        {
            emailConfig = emailConfiguration;
        }

        public void SendEmail(EmailMessage emailMessage)
        {
            //var message = new MimeMessage();
            //message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
            //message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

            //message.Subject = emailMessage.Subject;

            //message.Body = new TextPart(TextFormat.Html)
            //{
            //    Text = emailMessage.Content
            //};

            //using (var emailClient = new SmtpClient())
            //{
            //    // true uses SSL
            //    emailClient.Connect(emailConfig.SmtpServer, emailConfig.SmtpPort, true);

            //    //Remove any OAuth functionality
            //    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

            //    emailClient.Authenticate(emailConfig.SmtpUsername, emailConfig.SmtpPassword);

            //    emailClient.Send(message);

            //    emailClient.Disconnect(true);
        }
    }
}

