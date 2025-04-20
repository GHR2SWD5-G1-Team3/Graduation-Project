
using System.Net.Mail;
using System.Net;

namespace BLL.Services.Implementation
{
    public class EmailSender : IEmailSender
    {
        private readonly string _smtpHost = "smtp.gmail.com"; // Your SMTP host
        private readonly int _smtpPort = 587; // Your SMTP port
        private readonly string _smtpUser = "your-email@gmail.com"; // Your email address
        private readonly string _smtpPass = "your-email-password"; // Your email password

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using (var client = new SmtpClient(_smtpHost, _smtpPort))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpUser),
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