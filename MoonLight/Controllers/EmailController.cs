using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoonLight.API.Errors;

namespace MoonLight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailConfirmationService _emailConfirmationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmailController(
            IEmailConfirmationService emailConfirmationService,
            UserManager<ApplicationUser> userManager)
        {
            _emailConfirmationService = emailConfirmationService;
            _userManager = userManager;
        }

        [HttpGet("confirm")]
        public async Task<ActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token, string csrfToken)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
                return BadRequest(new ApiResponse(400, "Email or token is missing"));

            var (success, message) = await _emailConfirmationService.ConfirmEmailAsync(email, token, csrfToken);
            if (!success)
                return BadRequest(new ApiResponse(400, message));

            return Ok(new ApiResponse(200, message));
        }

        [HttpPost("resend-confirmation")]
        public async Task<ActionResult> ResendConfirmationEmail([FromQuery] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound(new ApiResponse(404, "User not found"));

            if (await _userManager.IsEmailConfirmedAsync(user))
                return BadRequest(new ApiResponse(400, "Email is already confirmed"));

            var (success, message) = await _emailConfirmationService.SendConfirmationEmailAsync(user);
            if (!success)
                return BadRequest(new ApiResponse(400, message));

            return Ok(new ApiResponse(200, "Confirmation email sent successfully"));
        }
    }
}