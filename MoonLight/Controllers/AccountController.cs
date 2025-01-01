using AutoMapper;
using Azure.Core;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.SqlServer.Server;
using MoonLight.API.DTOs;
using MoonLight.API.Errors;
using System.Security.Claims;

namespace MoonLight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IEmailConfirmationService _emailConfirmationService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IResetPasswordEmailService _resetPasswordEmailService;
        private readonly IGoogleAuthService _googleAuthService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ITokenService tokenService,
            IEmailConfirmationService emailConfirmationService, IResetPasswordEmailService resetPasswordEmailService, 
            IGoogleAuthService googleAuthService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailConfirmationService = emailConfirmationService;
            _resetPasswordEmailService = resetPasswordEmailService;
            _googleAuthService = googleAuthService;
        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;

            var user = await _userManager.FindByEmailAsync(email);

            return new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };

        }

        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> CheckEmailExists([FromQuery] string email)
        {
            return Ok(await _userManager.FindByEmailAsync(email) != null);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginInfo)
        {
            var user = await _userManager.FindByEmailAsync(loginInfo.Email);

            if (user == null)
                return Unauthorized(new ApiResponse(401));

            var isConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!isConfirmed)
            {
                var (success, message) = await _emailConfirmationService.SendConfirmationEmailAsync(user);
                return Unauthorized(new ApiResponse(401, "Email not confirmed. A new confirmation email has been sent."));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginInfo.Password, false);
            if (!result.Succeeded)
                return Unauthorized(new ApiResponse(401));


            return new UserDTO
            {
                Email = loginInfo.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("SignIn-Google")]
        public async Task<ActionResult<UserDTO>> SignInWithGoogle([FromBody] GoogleLoginDTO request)
        {
            var payload = await _googleAuthService.VerifyGoogleToken(request.IdToken);

            var user = await _userManager.FindByEmailAsync(payload.Email);

            if (user != null)
            {
                return Ok(
                    new UserDTO
                    {
                        DisplayName = user.DisplayName,
                        Email = user.Email,
                        Token = _tokenService.CreateToken(user)
                    });
            }

            // Create new user if they don't exist
            user = new ApplicationUser
            {
                DisplayName = payload.Name,
                Email = payload.Email,
                UserName = payload.Email,
                EmailConfirmed = payload.EmailVerified
            };

            var createResult = await _userManager.CreateAsync(user);

            if (!createResult.Succeeded)
            {
                return BadRequest(new ApiResponse(400, "Error creating user" ));
            }

            var existingLogins = await _userManager.GetLoginsAsync(user);
            var googleLogin = existingLogins.FirstOrDefault(login => login.LoginProvider == "Google");

            if (googleLogin == null)
            {
                // Link the Google account with the user
                var addLoginResult = await _userManager.AddLoginAsync(user, new UserLoginInfo(
                    loginProvider: "Google",
                    providerKey: payload.Subject, // This is the Google user ID
                    displayName: payload.Name
                ));

                if (!addLoginResult.Succeeded)
                {
                    return BadRequest(new ApiResponse(404, "Error linking Google account" ));
                }
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            

            return Ok(
                new UserDTO
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                });

        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registeredUser)
        {
            if (CheckEmailExists(registeredUser.Email).Result.Value)
            {
                return new BadRequestObjectResult(
                    new ApiValidation { Errors = new[] { "Email is already in use" } }
                );
            }

            var user = new ApplicationUser
            {
                Email = registeredUser.Email,
                DisplayName = registeredUser.DisplayName,
                UserName = registeredUser.UserName,
                PhoneNumber = registeredUser.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, registeredUser.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return BadRequest(new ApiResponse(400, errors));
            }

            var (success, message) = await _emailConfirmationService.SendConfirmationEmailAsync(user);
            if (!success)
                return BadRequest(new ApiResponse(400, message));

            return Ok(
            new UserDTO
                {
                    DisplayName = registeredUser.DisplayName,
                    Email = registeredUser.Email,
                    Token = _tokenService.CreateToken(user),
                    Confirm = "Please confirm your email to complete registration"
                }
            );
        }

        [Authorize]
        [HttpGet("SignOut")]
        public async Task<ActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost("forget-password")]
        public async Task<ActionResult> ForgetPassword(ForgetPasswordDTO model)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return BadRequest(new ApiResponse(400, "Invalid Request"));
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var param = new Dictionary<string, string?>
            {
                {"token", token },
                {"email", model.Email }
            };

            string callbackURL = QueryHelpers.AddQueryString(model.ClientURL,param);

            var (result,message) = await _resetPasswordEmailService.SendResetPasswordEmail(model.Email, callbackURL);

            if (!result)
            {
                return BadRequest(new ApiResponse(400, message));
            }

            return Ok(new ApiResponse(200, message));
        }

        [Authorize]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return BadRequest(new ApiResponse(400, "Invalid Request"));
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if(!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return BadRequest(new ApiResponse(400, errors));
            }

            return Ok(new ApiResponse(200, "Reset Password Done!"));
        }

    }
}
