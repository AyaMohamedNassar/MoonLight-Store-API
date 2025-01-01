using Core.Interfaces;
using Core.Models;
using Core.Services;
using Infrastructure.Data;
using Infrastructure.Repository;
using Infrastructure.Services;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MoonLight.API.Errors;
using MoonLight.API.Helpers.Mapper;

namespace MoonLight.API.Extentions
{
    public static class ApplicationServicesExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            //Options For Validation Errors - Override the default Behavior
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(e => e.Value?.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidation
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            //DI
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            Services.AddScoped<ITokenService, TokenService>();
            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();



            //AutoMapper
            AutoMapperConfiguration.Configure(Services);

            // Add CORS
            Services.AddCors(options =>
            {
                options.AddPolicy("CORSPolicy", policy => 
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
            });
            return Services;
        }

        public static IServiceCollection AddEmailServices(this IServiceCollection services, EmailConfiguration emailConfig)
        {
            services.Configure<EmailConfiguration>(options =>
            {
                options.SmtpServer = emailConfig.SmtpServer;
                options.SmtpPort = emailConfig.SmtpPort;
                options.SmtpUsername = emailConfig.SmtpUsername;
                options.SmtpPassword = emailConfig.SmtpPassword;
                options.FromEmail = emailConfig.FromEmail;
                options.FromName = emailConfig.FromName;
                options.EnableSsl = emailConfig.EnableSsl;
            });

            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IResetPasswordEmailService, ResetPasswordEmailService>();
            services.AddScoped<IEmailConfirmationService, EmailConfirmationService>();
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
