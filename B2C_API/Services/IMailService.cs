using B2C_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2C_API.Services
{
    public interface IMailService
    {
        //Task SendEmailAsync(MailRequest mailRequest);
        Task SendWelcomeEmailAsync(Request request);
    }
}
