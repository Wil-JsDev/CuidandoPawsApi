using AutoMapper;
using CuidandoPawsApi.Application.DTOs.Appoinment;
using CuidandoPawsApi.Application.DTOs.ServiceCatalog;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Domain.Ports.UseCase.Appoinment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.Adapters.Appointment
{
    public class CheckAppoinmentAvailability : ICheckAppoinmentAvailability<ServiceCatalogDTos>
    {
        private readonly IAppoinmentRepository _appoinmentRepository;
        private readonly IMapper _mapper;
        private readonly IServiceCatalogRepository _serviceCatalogRepository;

        public CheckAppoinmentAvailability(IAppoinmentRepository appoinmentRepository, IMapper mapper, IServiceCatalogRepository serviceCatalogRepository)
        {
            _appoinmentRepository = appoinmentRepository;
            _mapper = mapper;
            _serviceCatalogRepository = serviceCatalogRepository;
        }

        public async Task<IEnumerable<ServiceCatalogDTos>> CheckAvailabilityAsync(int serviceId, CancellationToken cancellationToken)
        {
            var serviceCatalog = await _serviceCatalogRepository.GetByIdAsync(serviceId,cancellationToken);

            if (serviceCatalog == null)
            {
                return Enumerable.Empty<ServiceCatalogDTos>(); //Enumerable null
            }
            serviceCatalog.IsAvaible = true;

            var availableServiceCatalog =  await _appoinmentRepository.CheckAvailabilityAsync(serviceCatalog.Id,cancellationToken);
                
            var serviceCatalogDto = _mapper.Map<IEnumerable<ServiceCatalogDTos>>(availableServiceCatalog);

            return serviceCatalogDto;
        }
    }
}
