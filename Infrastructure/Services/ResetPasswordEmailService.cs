using Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ResetPasswordEmailService : IResetPasswordEmailService
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<ResetPasswordEmailService> _logger;
        public ResetPasswordEmailService(IEmailService emailService, ILogger<ResetPasswordEmailService> logger) 
        {
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<(bool, string)> SendResetPasswordEmail(string email, string callbackURL)
        {
            string htmlContent =$@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;'>
                    <h2 style='color: #333;'>Welcome to MoonLight!</h2>
                    <p>Please Reset Your Password By Click the link below</p>
                    <div style='margin: 25px 0;'>
                        <a href='{WebUtility.HtmlEncode(callbackURL)}' 
                            style='background-color: #4CAF50; color: white; padding: 12px 24px; 
                                    text-decoration: none; border-radius: 4px; display: inline-block;'>
                            Reset Your Password
                        </a>
                    </div>
                    <p style='color: #666; font-size: 14px;'>
                        If you didn't request this, you can safely ignore this email.
                    </p>
                    <hr style='border: 1px solid #eee; margin: 20px 0;'>
                    <p style='color: #999; font-size: 12px;'>
                        This is an automated message, please do not reply.
                    </p>
                </div>";

            bool result = await _emailService.SendEmailAsync(email, "Reset Password", htmlContent);

            if (result) {
                _logger.LogInformation($"Email Reset Password link sent to {email}");

                return (true, "Reset Password email sent successfully");
            }
            else
            {
                return (false, "An error occurred. Please try again later.");
            }
        }
    }
}
