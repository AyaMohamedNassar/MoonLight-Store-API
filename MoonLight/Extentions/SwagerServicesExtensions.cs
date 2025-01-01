using Microsoft.OpenApi.Models;

namespace MoonLight.API.Extentions
{
    public static class SwagerServicesExtensions
    {
        public static IServiceCollection AddSwagerDocumentations(this IServiceCollection Services) 
        {
            Services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();

                options.SwaggerDoc("MoonLightShopAPIv1", new OpenApiInfo
                {
                    Title = "MoonLight Shop",
                    Version = "v1",
                    Description = "MoonLight Web API Application",
                    Contact = new OpenApiContact
                    {
                        Name = "Aya Nassar",
                        Email = "aya.m.nasar@gmail.com",
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Aya Nassar",
                    }
                });

                var securitySchema = new OpenApiSecurityScheme()
                {
                    Description = "JWT Authentication Bearer Schema",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    }
                };

                options.AddSecurityDefinition("Bearer", securitySchema);

                var securitRequirments = new OpenApiSecurityRequirement() { { securitySchema, new[] {"Bearer" } } };

                options.AddSecurityRequirement(securitRequirments);
            });

            return Services;
        }

        public static IApplicationBuilder UseSwagerDocumentation(this IApplicationBuilder app) 
        {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/MoonLightShopAPIv1/swagger.json", "MoonLight Shop API v1");
                });

            return app;
        }
    }
}
