﻿using CuidandoPawsApi.Application.Adapters.Appointment;
using CuidandoPawsApi.Application.Adapters.MedicalRecordAdapt;
using CuidandoPawsApi.Application.DTOs.Appoinment;
using CuidandoPawsApi.Application.DTOs.MedicalRecord;
using CuidandoPawsApi.Application.DTOs.ServiceCatalog;
using CuidandoPawsApi.Application.Mapper;
using CuidandoPawsApi.Domain.Ports.UseCase.Appoinment;
using CuidandoPawsApi.Domain.Ports.UseCase.MedicalRecord;
using Microsoft.Extensions.DependencyInjection;

namespace CuidandoPawsApi.Application.IOC
{
    public static class AddApplication
    {
        public static void AddApplicationService(IServiceCollection services)
        {

            #region Mapper
            services.AddAutoMapper(typeof(MappingProfile));
            #endregion

            #region Appoinment
            services.AddScoped<ICreateAppoinment<CreateUpdateAppoinmentDTos, AppoinmentDTos>, CreateAppoinment>();
            services.AddScoped<ICheckAppoinmentAvailability<ServiceCatalogDTos>, CheckAppoinmentAvailability>();
            services.AddScoped<IGetByIdAppoinment<AppoinmentDTos>, GetbyIdAppoinment>();
            services.AddScoped<IGetAppoinmentLastAddedOndate<ServiceCatalogDTos>, GetAppoinmentLastAddedOnDate>();
            services.AddScoped<IGetAppoinmentAvailabilityService<ServiceCatalogDTos>, GetAppoinmentAvailabilityService>();
            services.AddScoped<IUpdateAppoinment<CreateUpdateAppoinmentDTos, AppoinmentDTos>>();    
            services.AddScoped<IGetAppoinment<AppoinmentDTos>,GetAllAppoinment>();
            services.AddScoped<IDeleteAppoinment<AppoinmentDTos>, DeleteAppoinment>();
            #endregion

            #region MedicalRecord
            services.AddScoped<ICreateMedicalRecord<CreateUpdateMedicalRecordDTos, MedicalRecordDTos>, CreateMedicaRecord>();
            services.AddScoped<IGetMedicalRecord<MedicalRecordDTos>, GetMedicalRecord>();
            services.AddScoped<IDeleteMedicalRecord<MedicalRecordDTos>, DeleteMedicalRecord>();    
            services.AddScoped<IGetByIdMedicalRecord<MedicalRecordDTos>,GetByIdMedicalRecord>();
            services.AddScoped<IUpdateMedicalRecord<CreateUpdateMedicalRecordDTos,MedicalRecordDTos>, UpdateMedicalRecord>();
            #endregion

        }
    }
}
