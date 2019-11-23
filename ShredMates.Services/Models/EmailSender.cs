using ShredMates.Services.Interfaces;
using System.Threading.Tasks;

namespace ShredMates.Web.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}
