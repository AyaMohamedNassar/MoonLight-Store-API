using System.Security.Claims;

namespace MoonLight.API.Extentions
{
    public static class ClaimsPrinciplesExtentions
    {
        public static string RetriveEmail(this ClaimsPrincipal user)
        {
            return user?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
        }
    }
}
