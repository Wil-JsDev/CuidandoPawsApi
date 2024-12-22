using CuidandoPawsApi.Application.DTOs.Appoinment;
using CuidandoPawsApi.Infrastructure.Api.Validations.Appoinment;
using FluentValidation;

namespace CuidandoPawsApi.Infrastructure.Api.Extensions
{
    public static class AddValidator
    {
        public static void AddValidations(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateUpdateAppoinmentDTos>, CreateUpdateAppoinment>();
            
        }
    }
}
