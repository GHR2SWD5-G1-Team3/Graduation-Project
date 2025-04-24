using System.Net.Mail;
using System.Net;
using BLL.Configration;
using Microsoft.Extensions.Options;

namespace BLL.Services.Implementation
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using (var client = new SmtpClient(_emailSettings.SmtpHost, _emailSettings.SmtpPort))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(_emailSettings.SmtpUser, _emailSettings.SmtpPass);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.SmtpUser),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(toEmail);

                await client.SendMailAsync(mailMessage);
            }
        }
    }

}