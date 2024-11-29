using CuidandoPawsApi.Application.DTOs.Email;
using CuidandoPawsApi.Domain.Ports.Email;
using CuidandoPawsApi.Infrastructure.Shared.Adapter;
using Microsoft.Extensions.DependencyInjection;

namespace CuidandoPawsApi.Infrastructure.Shared
{
    public static class AddShared
    {
        public static void AddSharedLayer(this IServiceCollection services)
        {
            services.AddScoped<IEmailSender<EmailRequestDTos>, EmailSender>();
        }
    }
}
