using Microsoft.AspNetCore.Mvc;

namespace PLL.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task<IActionResult> SendTestEmail()
        {
            var toEmail = "recipient@example.com";
            var subject = "Test Email";
            var body = "<h1>This is a test email sent from ASP.NET Core</h1>";

            try
            {
                await _emailSender.SendEmailAsync(toEmail, subject, body);
                return Ok("Email sent successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}
