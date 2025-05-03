using System.Net.Mail;
using System.Net;

namespace InventorySystem.Service
{
    public class EmailService:IEmailService
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public void SendEmail(string to, string subject, string body)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(
                    _configuration["EmailSettings:Email"],
                    _configuration["EmailSettings:AppPassword"]
                ),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["EmailSettings:Email"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = false,
            };

            mailMessage.To.Add(to);

            try
            {
                smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation("Real email sent to " + to);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to send email: " + ex.Message);
            }
        }

    }
}
