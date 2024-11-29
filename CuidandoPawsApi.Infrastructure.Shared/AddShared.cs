using CuidandoPawsApi.Application.DTOs.Email;
using CuidandoPawsApi.Domain.Ports.Email;
using CuidandoPawsApi.Domain.Settings;
using CuidandoPawsApi.Infrastructure.Shared.Adapter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CuidandoPawsApi.Infrastructure.Shared
{
    public static class AddShared
    {
        public static void AddSharedLayer(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IEmailSender<EmailRequestDTos>, EmailSender>();
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        }
    }
}
