using ShredMates.Services.Common;
using ShredMates.Services.Models;

namespace ShredMates.Services.Interfaces
{
    public interface IEmailService : ISingletonService
    {
        void SendEmail(EmailMessage emailMessage);
    }
}
