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
//using AppoinmentDTos = CuidandoPawsApi.Application.DTOs.Appoinment.AppoinmentDTos;

namespace CuidandoPawsApi.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Appoinment
            CreateMap<Appoinment, CreateUpdateAppoinmentDTos>()
                                  .ReverseMap();

            CreateMap<Appoinment, AppoinmentDTos>()
                      .ForMember(dest => dest.AppoinmentId, opt => opt.MapFrom(src => src.Id));

            CreateMap<AppoinmentDTos, Appoinment>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AppoinmentId));

            #endregion

            #region ServiceCatalog
            CreateMap<ServiceCatalog, ServiceCatalogDTos>()
                        .ForMember(dest => dest.ServiceCatalogId, opt => opt.Ignore())
                        .ForMember(dest => dest.DescriptionService, opt => opt.Ignore());

            CreateMap<ServiceCatalogDTos, ServiceCatalog>()
                        .ForMember(dest => dest.Id, opt => opt.Ignore())
                        .ForMember(dest => dest.Description, opt => opt.Ignore());
            #endregion

            #region Medical Record
            CreateMap<CreateUpdateMedicalRecordDTos, MedicalRecord>()
                                                     .ReverseMap();

            CreateMap<MedicalRecord, MedicalRecordDTos>()
                        .ForMember(opt => opt.MedicalRecordId, src => src.MapFrom(src => src.Id));

            CreateMap<MedicalRecordDTos, MedicalRecord>()
                        .ForMember(opt => opt.Id, src => src.MapFrom(src => src.MedicalRecordId));
            #endregion
        }
    }
}
