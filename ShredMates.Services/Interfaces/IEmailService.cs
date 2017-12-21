using ShredMates.Services.Models;

namespace ShredMates.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(EmailMessage emailMessage);
    }
}
