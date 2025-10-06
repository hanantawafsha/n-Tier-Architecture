using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace NTierArchitecture.PL.Utilities
{
    public class EmailSetting : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("hanantawafsha@gmail.com", "ipbj vigi jjqq xxoj")
            };

            return client.SendMailAsync(
                new MailMessage(from: "hanantawafsha@gmail.com",
                                to: email,
                                subject,
                                htmlMessage
                                )
                { IsBodyHtml=true});


        }
    }
}
