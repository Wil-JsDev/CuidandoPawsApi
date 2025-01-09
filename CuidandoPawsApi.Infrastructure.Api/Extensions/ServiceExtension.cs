using Asp.Versioning;
using Microsoft.OpenApi.Models;

namespace CuidandoPawsApi.Infrastructure.Api.Extensions
{
    public static class ServiceExtension
    {
        
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Cuidando Paws Api",
                    Description = "API for managing pet care services, including registration, appointments, and medical records",
                    Contact = new OpenApiContact
                    {
                        Name = "Wilmer Jose De La Cruz",
                        Email = "wilmerdelacruz.dev@gmail.com"
                    }
                });

                option.DescribeAllParametersInCamelCase();
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here}"
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        }, new List<string>()
                    },
                });
            });

        }

        public static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1,0);
                options.AssumeDefaultVersionWhenUnspecified = true; //When no versions are sent, this assumes the default version which is V1
                options.ReportApiVersions = true;
            });
        }
    }
}
