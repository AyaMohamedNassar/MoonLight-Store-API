using Core.Entities.Identity;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MoonLight.API.Extentions
{
    public static class IdentityServiceExtentions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {

            var builder = services.AddIdentity<ApplicationUser, IdentityRole>(
                 options => {
                     options.User.RequireUniqueEmail = true;
                     options.SignIn.RequireConfirmedEmail = true; 
                 }
            );


            builder.AddEntityFrameworkStores<StoreDbContext>();

            builder.AddSignInManager<SignInManager<ApplicationUser>>();

            builder.AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(10);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:key"])),
                    ValidIssuer = configuration["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            }).AddGoogle(options =>
                {
                    options.ClientId = configuration["Authentication:Google:client_id"];
                    options.ClientSecret = configuration["Authentication:Google:client_secret"];
                }); ;

            return services;
        }
    }
}
