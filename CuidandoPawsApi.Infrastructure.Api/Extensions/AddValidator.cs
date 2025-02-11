﻿using CuidandoPawsApi.Application.DTOs.Account.Authenticate;
using CuidandoPawsApi.Application.DTOs.Account.Password.Forgot;
using CuidandoPawsApi.Application.DTOs.Account.Password.Reset;
using CuidandoPawsApi.Application.DTOs.Account.Register;
using CuidandoPawsApi.Application.DTOs.Appoinment;
using CuidandoPawsApi.Application.DTOs.MedicalRecord;
using CuidandoPawsApi.Application.DTOs.Pets;
using CuidandoPawsApi.Application.DTOs.ServiceCatalog;
using CuidandoPawsApi.Application.DTOs.Species;
using CuidandoPawsApi.Infrastructure.Api.Validations.Account;
using CuidandoPawsApi.Infrastructure.Api.Validations.Appoinment;
using CuidandoPawsApi.Infrastructure.Api.Validations.MedicalRecord;
using CuidandoPawsApi.Infrastructure.Api.Validations.Pets;
using CuidandoPawsApi.Infrastructure.Api.Validations.ServiceCatalog;
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
            services.AddScoped<IValidator<CreateUpdateMedicalRecordDTos>, CreateUpdateMedicalRecord>();
            services.AddScoped<IValidator<CreatePetsDTos>, CreatePets>();
            services.AddScoped<IValidator<UpdatePetsDTos>, UpdatePets>();
            services.AddScoped<IValidator<CreateServiceCatalogDTos>,CreateServiceCatalogValidation>();
            services.AddScoped<IValidator<UpdateServiceCatalogDTos>, UpdateServiceCatalogValidation>();
            services.AddScoped<IValidator<AuthenticateRequest>, AuthenticateRequestValidation>();
            services.AddScoped<IValidator<ForgotRequest>, ForgotRequestValidation>();   
            services.AddScoped<IValidator<ResetPasswordRequest>, ResetPasswordRequestValidation>();
            services.AddScoped<IValidator<RegisterRequest>, RegisterRequestValidation>();
        }
    }
}
