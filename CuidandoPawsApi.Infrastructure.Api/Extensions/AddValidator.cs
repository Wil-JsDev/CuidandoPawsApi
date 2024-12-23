﻿using CuidandoPawsApi.Application.DTOs.Appoinment;
using CuidandoPawsApi.Application.DTOs.Species;
using CuidandoPawsApi.Infrastructure.Api.Validations.Appoinment;
using CuidandoPawsApi.Infrastructure.Api.Validations.Species;
using FluentValidation;

namespace CuidandoPawsApi.Infrastructure.Api.Extensions
{
    public static class AddValidator
    {
        public static void AddValidations(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateUpdateAppoinmentDTos>, CreateUpdateAppoinment>();
            services.AddScoped<IValidator<CreateUpdateSpecieDTos>, CreateUpdateSpecies>();
        }
    }
}
