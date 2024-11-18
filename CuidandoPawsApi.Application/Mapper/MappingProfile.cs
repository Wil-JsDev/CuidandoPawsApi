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

            CreateMap<AppoinmentDTos, Appoinment>();

            #endregion

            #region ServiceCatalog
            CreateMap<ServiceCatalog, ServiceCatalogDTos>()
                        .ForMember(dest => dest.ServiceCatalogId, opt => opt.Ignore())
                        .ForMember(dest => dest.DescriptionService, opt => opt.Ignore());

            CreateMap<ServiceCatalogDTos, ServiceCatalog>();
            #endregion

        }
    }
}
