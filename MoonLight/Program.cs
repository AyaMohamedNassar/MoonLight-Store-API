using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Serilog;
using MoonLight.API.Middleware;
using MoonLight.API.Extentions;
using StackExchange.Redis;
using Microsoft.AspNetCore.Identity;
using Core.Entities.Identity;
using Core.Models;
using Microsoft.Extensions.Configuration;

namespace MoonLight.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration); 

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwagerDocumentations();

            //Add DbContext
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("MoonLightConnStr"));
            });

            //Redis
            builder.Services.AddSingleton<IConnectionMultiplexer>(config => {
                var configurations = ConfigurationOptions
                    .Parse(builder.Configuration.GetConnectionString("Redis"), true);

                return ConnectionMultiplexer.Connect(configurations);
            });

            //Using Serilog
            builder.Host.UseSerilog((context, loggerConfig) =>
                loggerConfig.ReadFrom.Configuration(context.Configuration));

            // Email Service
            var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            builder.Services.AddEmailServices(emailConfig);

            var app = builder.Build();

             // Automatically apply migrations, Seeding Data
            if (app.Environment.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var service = scope.ServiceProvider;

                    var loggerFactory = service.GetRequiredService<ILoggerFactory>();

                    var logger = loggerFactory.CreateLogger<Program>();

                    try
                    {
                        var dbContext = service.GetRequiredService<StoreDbContext>();
                        var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();

                        await dbContext.Database.MigrateAsync();

                        //Apply Seeding
                        await StoreContextSeed.SeedAsync(dbContext, loggerFactory);
                        await StoreContextSeed.SeedUserAsync(userManager);

                        logger.LogInformation("Database migrations applied successfully.");

                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An Error Occuers during database migrations.");

                    }

                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagerDocumentation();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseSerilogRequestLogging();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("CORSPolicy");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
