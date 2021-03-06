﻿using ShredMates.Services.Common;
using System.Threading.Tasks;

namespace ShredMates.Services.Interfaces
{
    public interface IEmailSender : ISingletonService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
