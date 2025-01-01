using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MoonLight.API.Extentions
{
    public static class UserManagerExtenstions
    {
        public static async Task<ApplicationUser> GetUserWithAdsressAsync(this UserManager<ApplicationUser> input,
            ClaimsPrincipal user)
        {
            string? email = user?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;

            return await input.Users.Include(x => x.Address).SingleOrDefaultAsync(user => user.Email == email);

        }

        public static async Task<ApplicationUser> FindByEmailFromClaimsPrincipalAsync(this UserManager<ApplicationUser> input,
            ClaimsPrincipal user)
        {
            string? email = user?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;

            return await input.Users.SingleOrDefaultAsync(user => user.Email == email);

        }

    }
}
