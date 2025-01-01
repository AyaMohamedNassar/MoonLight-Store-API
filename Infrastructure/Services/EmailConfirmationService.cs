using Core.Entities.Identity;
using Core.Interfaces;
using Core.Specifications.EmailConfirmationTokenSpecification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using System.Web;

namespace Infrastructure.Services
{
    public class EmailConfirmationService : IEmailConfirmationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<EmailConfirmationService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public EmailConfirmationService(
            UserManager<ApplicationUser> userManager,
            IEmailService emailService,
            IHttpContextAccessor httpContextAccessor, 
            ILogger<EmailConfirmationService> logger, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _userManager = userManager;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }


        public async Task<(bool success, string message)> SendConfirmationEmailAsync(ApplicationUser user)
        {
            try
            {
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                HttpRequest request = _httpContextAccessor.HttpContext.Request;

                string csrfToken = Guid.NewGuid().ToString("N");

                string hashedToken = HashToken(token);

                EmailConfirmationToken emailConfirmationToken = new (user.Id, hashedToken, csrfToken, DateTime.Now);

                 _unitOfWork.Repository<EmailConfirmationToken>().Add(emailConfirmationToken);

                int result = await _unitOfWork.SaveAsync();

                if (result == 0)
                {
                    _logger.LogError("EmailConfirmationToken, Erorr Will Adding the Entity");
                    return (false, "Error While Sending the Email");
                }

                var callbackUrl = new UriBuilder
                {
                    Scheme = request.Scheme,
                    Host = request.Host.Host,
                    Port = request.Host.Port ?? (request.Scheme == "https" ? 443 : 80),
                    Path = "/api/email/confirm",
                    Query = $"email={HttpUtility.UrlEncode(user.Email)}&token={HttpUtility.UrlEncode(token)}&csrfToken={csrfToken}"
                }.ToString();


                _logger.LogInformation("Call Back Url: " + callbackUrl);


                await _emailService.SendEmailAsync(
                        user.Email,
                        "Confirm Your Email - MoonLight",
                        GenerateEmailTemplate(user.DisplayName, callbackUrl)
                    );

                _logger.LogInformation($"Email confirmation link sent to {user.Email}");

                return (true, "Confirmation email sent successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while sending confirmation email");
                return (false, "An error occurred. Please try again later.");
            }
        }

        public async Task<(bool success, string message)> ConfirmEmailAsync(string email, string token, string csrfToken)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return (false, "User not found");

            EmailConfirmationTokenSpec tokenSpec = new EmailConfirmationTokenSpec(user.Id);

            var tokenEntity = await _unitOfWork.Repository<EmailConfirmationToken>().GetEntity(tokenSpec);

            string hashedToken = HashToken(token);

            if (hashedToken != tokenEntity.HashedToken || csrfToken != tokenEntity.CsrfToken)
            {
                return (false, "Invalid or expired token");
            }  

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                tokenEntity.IsUsed = true;

                int resutl = await _unitOfWork.SaveAsync();

                if (resutl == 0) {
                    _logger.LogError("EmailConfirmationToken isUsed Property did not update");
                }

                return (true, "Email confirmed successfully");
            }

            return (false, "Failed to confirm email: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        private string GenerateEmailTemplate(string displayName, string callbackUrl)
        {
            string encodedDisplayName = WebUtility.HtmlEncode(displayName);
            string encodedCallbackUrl = WebUtility.HtmlEncode(callbackUrl);


            return $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;'>
                    <h2 style='color: #333;'>Welcome to MoonLight, {encodedDisplayName}!</h2>
                    <p>Thank you for joining our store. To get started, please confirm your email address.</p>
                    <div style='margin: 25px 0;'>
                        <a href='{encodedCallbackUrl}' 
                            style='background-color: #4CAF50; color: white; padding: 12px 24px; 
                                    text-decoration: none; border-radius: 4px; display: inline-block;'>
                            Confirm Email
                        </a>
                    </div>
                    <p style='color: #666; font-size: 14px;'>
                        If you didn't create this account, you can safely ignore this email.
                    </p>
                    <hr style='border: 1px solid #eee; margin: 20px 0;'>
                    <p style='color: #999; font-size: 12px;'>
                        This is an automated message, please do not reply.
                    </p>
                </div>";
        }

        private string HashToken(string token) 
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_configuration["Token:key"]));
            var hashedBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(token));
            return Convert.ToBase64String(hashedBytes);
        }
    }
    
}
