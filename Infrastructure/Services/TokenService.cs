using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = new(Encoding.UTF8.GetBytes(_configuration["Token:key"]));
        }
        public string CreateToken(ApplicationUser user)
        {
            var clamis = new List<Claim>() { 
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.GivenName, user.DisplayName),
            };

            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor() 
            {
                Subject = new ClaimsIdentity(clamis),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = cred,
                Issuer = _configuration["Token:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
