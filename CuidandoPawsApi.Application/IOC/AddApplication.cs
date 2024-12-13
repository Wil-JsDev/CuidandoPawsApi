using CuidandoPawsApi.Application.Adapters.Appointment;
using CuidandoPawsApi.Application.Adapters.MedicalRecordAdapt;
using CuidandoPawsApi.Application.Adapters.PetsAdapt;
using CuidandoPawsApi.Application.Adapters.ServiceCatalogAdapt;
using CuidandoPawsApi.Application.Adapters.SpeciesAdapt;
using CuidandoPawsApi.Application.Adapters.SpeciesAdapter;
using CuidandoPawsApi.Application.DTOs.Appoinment;
using CuidandoPawsApi.Application.DTOs.MedicalRecord;
using CuidandoPawsApi.Application.DTOs.Pets;
using CuidandoPawsApi.Application.DTOs.ServiceCatalog;
using CuidandoPawsApi.Application.DTOs.Species;
using CuidandoPawsApi.Application.Mapper;
using CuidandoPawsApi.Domain.Ports.UseCase;
using CuidandoPawsApi.Domain.Ports.UseCase.Appoinment;
using CuidandoPawsApi.Domain.Ports.UseCase.MedicalRecord;
using CuidandoPawsApi.Domain.Ports.UseCase.Pets;
using CuidandoPawsApi.Domain.Ports.UseCase.ServiceCatalog;
using CuidandoPawsApi.Domain.Ports.UseCase.Species;
using Microsoft.Extensions.DependencyInjection;

namespace CuidandoPawsApi.Application.IOC
{
    public static class AddApplication
    {
        public static void AddApplicationService(this IServiceCollection services)
        {

            #region Mapper
            services.AddAutoMapper(typeof(MappingProfile));
            #endregion

            #region Pets
            services.AddScoped<ICreatePets<CreatePetsDTos, PetsDTos>, CreatePets>();
            services.AddScoped<IGetByIdPets<PetsDTos>, GetByIdPets>();
            services.AddScoped<IGetPets<PetsDTos>,GetPets>();
            services.AddScoped<IGetPagedPets<PetsDTos>, GetPetsPaged>();
            services.AddScoped<IDeletePets<PetsDTos>, DeletePets>();    
            services.AddScoped<IUpdatePets<UpdatePetsDTos,PetsDTos>, UpdatePets>();
            services.AddScoped<IGetPetsLastAddedOfDay<PetsDTos>, GetPetsLastAddedOfDay>();
            #endregion

            #region Appoinment
            services.AddScoped<ICreateAppoinment<CreateUpdateAppoinmentDTos, AppoinmentDTos>, CreateAppoinment>();
            services.AddScoped<ICheckAppoinmentAvailability<ServiceCatalogDTos>, CheckAppoinmentAvailability>();
            services.AddScoped<IGetByIdAppoinment<AppoinmentDTos>, GetbyIdAppoinment>();
            services.AddScoped<IGetAppoinmentLastAddedOndate<ServiceCatalogDTos>, GetAppoinmentLastAddedOnDate>();
            services.AddScoped<IGetAppoinmentAvailabilityService<ServiceCatalogDTos>, GetAppoinmentAvailabilityService>();
            services.AddScoped<IUpdateAppoinment<CreateUpdateAppoinmentDTos, AppoinmentDTos>,UpdateAppoinment>();    
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

            #region Species
            services.AddScoped<ICreateSpecies<CreateUpdateSpecieDTos, SpeciesDTos>, CreateSpecies>();
            services.AddScoped<IDeleteSpecies<SpeciesDTos>,DeleteSpecies>();
            services.AddScoped<IGetByIdSpecies<SpeciesDTos>,GetByIdSpecies>();
            services.AddScoped<IGetSpecies<SpeciesDTos>,GetSpecies>();
            services.AddScoped<IGetSpeciesLastAdded<SpeciesDTos>,GetSpeciesLastAdded>();
            services.AddScoped<IGetSpeciesOrderById<SpeciesDTos>,GetSpeciesOrderById>();
            services.AddScoped<IUpdateSpecies<CreateUpdateSpecieDTos,SpeciesDTos>,UpdateSpecies>();
            #endregion

            #region Service Catalog
            services.AddScoped<ICreateServiceCatalog<ServiceCatalogDTos, CreateServiceCatalogDTos>, CreateServiceCatalog>();
            services.AddScoped<IDeleteServiceCatalog<ServiceCatalogDTos>, DeleteServiceCatalog>();
            services.AddScoped<IUpdateServiceCatalog<ServiceCatalogDTos,UpdateServiceCatalogDTos>,UpdateServiceCatalog>();
            services.AddScoped<IGetServiceCatalog<ServiceCatalogDTos>, GetServiceCatalog>();
            services.AddScoped<IGetByIdServiceCatalog<ServiceCatalogDTos>,GetByIdServiceCatalog>();
            #endregion
        }
    }
}
