using AutoMapper;
using CuidandoPawsApi.Application.DTOs.ServiceCatalog;
using CuidandoPawsApi.Application.DTOs;
using CuidandoPawsApi.Application.DTOs.Appoinment;
using CuidandoPawsApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuidandoPawsApi.Application.DTOs.MedicalRecord;
using System.Runtime.ConstrainedExecution;
using CuidandoPawsApi.Application.DTOs.Species;
using CuidandoPawsApi.Application.DTOs.Pets;
using CuidandoPawsApi.Domain.Pagination;
//using AppoinmentDTos = CuidandoPawsApi.Application.DTOs.Appoinment.AppoinmentDTos;

namespace CuidandoPawsApi.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Appoinment
            CreateMap<Appoinment, CreateUpdateAppoinmentDTos>();

            CreateMap<CreateUpdateAppoinmentDTos, Appoinment>();

            CreateMap<Appoinment, AppoinmentDTos>()
                      .ForMember(dest => dest.AppoinmentId, opt => opt.MapFrom(src => src.Id))
                      .ForMember(dest => dest.ServiceCatalogId, opt => opt.MapFrom(src => src.IdServiceCatalog));
            

            CreateMap<AppoinmentDTos, Appoinment>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AppoinmentId));

            #endregion

            #region ServiceCatalog
            CreateMap<ServiceCatalog, ServiceCatalogDTos>()
                        .ForMember(dest => dest.ServiceCatalogId, src => src.MapFrom(src => src.Id))
                        .ForMember(dest => dest.DescriptionService, src => src.MapFrom(src => src.Description));

            CreateMap<ServiceCatalogDTos, ServiceCatalog>()
                        .ForMember(dest => dest.Id, opt => opt.Ignore())
                        .ForMember(dest => dest.Description, opt => opt.Ignore());

            CreateMap<CreateServiceCatalogDTos, ServiceCatalog>()
                        .ForMember(dest => dest.Description, src => src.MapFrom(src => src.DescriptionService));

            CreateMap<ServiceCatalog, CreateServiceCatalogDTos>()
                        .ForMember(dest => dest.DescriptionService, src => src.MapFrom(src => src.Description));

            CreateMap<UpdateServiceCatalogDTos, ServiceCatalog>()
                            .ForMember(dest => dest.Description, src => src.MapFrom(src => src.DescriptionService));

            CreateMap<ServiceCatalog,UpdateServiceCatalogDTos>()
                            .ForMember(dest => dest.DescriptionService, src => src.MapFrom(src => src.Description));
            #endregion

            #region Medical Record
            CreateMap<CreateUpdateMedicalRecordDTos, MedicalRecord>()
                                                     .ReverseMap();

            CreateMap<MedicalRecord, MedicalRecordDTos>()
                        .ForMember(opt => opt.MedicalRecordId, src => src.MapFrom(src => src.Id));

            CreateMap<MedicalRecordDTos, MedicalRecord>()
                        .ForMember(opt => opt.Id, src => src.MapFrom(src => src.MedicalRecordId));
            #endregion

            #region Species
            CreateMap<CreateUpdateSpecieDTos, Species>()
                            .ForMember(opt => opt.Description, src => src.MapFrom(src => src.DescriptionOfSpecies))
                            .ReverseMap();

            CreateMap<SpeciesDTos, Species>()
                            .ForMember(opt => opt.Id, src => src.MapFrom(src => src.SpeciesId))
                            .ForMember(opt => opt.Description, src => src.MapFrom(src => src.DescriptionOfSpecies));
                   
            CreateMap<Species, SpeciesDTos>()
                            .ForMember(opt => opt.SpeciesId, src => src.MapFrom(src => src.Id))
                            .ForMember(opt => opt.DescriptionOfSpecies, src => src.MapFrom(src => src.Description));
            #endregion

            #region Pets
            CreateMap<Pets, PetsDTos>()
                .ForMember(dest => dest.PetsId, src => src.MapFrom(src => src.Id))
                .ForMember(src => src.NotesPets, src => src.MapFrom(src => src.Notes));

            CreateMap<PetsDTos, Pets>()
               .ForMember(dest => dest.Id, src => src.MapFrom(src => src.PetsId))
               .ForMember(src => src.Notes, src => src.MapFrom(src => src.NotesPets));

            CreateMap<Pets, CreatePetsDTos>()
                .ForMember(src => src.NotesPets, src => src.MapFrom(src => src.Notes));

            CreateMap<CreatePetsDTos, Pets>()
                .ForMember(src => src.Notes, src => src.MapFrom(src => src.NotesPets));


            CreateMap<PagedResult<PetsDTos>, PagedResult<Pets>>();

            CreateMap<PagedResult<Pets>, PagedResult<PetsDTos>>();

            CreateMap<Pets, UpdatePetsDTos>();

            CreateMap<UpdatePetsDTos,Pets>();

            #endregion
        }
    }
}
