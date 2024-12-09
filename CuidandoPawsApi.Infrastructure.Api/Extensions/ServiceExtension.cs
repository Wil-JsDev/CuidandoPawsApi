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
                    Description = "this API is store",
                    Contact = new OpenApiContact
                    {
                        Name = "Wilmer Jose De La Cruz",
                        Email = "wilmerjosedelacruz65@gmail.com"
                    }
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
