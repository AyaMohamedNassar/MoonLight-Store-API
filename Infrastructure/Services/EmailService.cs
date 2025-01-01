using Core.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly ILogger<EmailService> _logger;
        private readonly SmtpClient _smtpClient;

        public EmailService(
            IOptions<EmailConfiguration> emailConfig,
            ILogger<EmailService> logger)
        {
            _emailConfig = emailConfig.Value;
            _logger = logger;

            _smtpClient = new SmtpClient
            {
                Host = _emailConfig.SmtpServer,
                Port = _emailConfig.SmtpPort,
                EnableSsl = _emailConfig.EnableSsl,
                Credentials = new NetworkCredential(_emailConfig.FromEmail, _emailConfig.SmtpPassword)
            };
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string htmlContent)
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailConfig.FromEmail, _emailConfig.FromName),
                    Subject = subject,
                    Body = htmlContent,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(to);

                await _smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation($"Email sent successfully to {to}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send email to {to}: {ex.Message}");
                return false;
            }
        }


    }
}