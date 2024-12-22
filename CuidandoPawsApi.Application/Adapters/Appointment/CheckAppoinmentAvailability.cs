using AutoMapper;
using CuidandoPawsApi.Application.DTOs.Appoinment;
using CuidandoPawsApi.Application.DTOs.ServiceCatalog;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Domain.Ports.UseCase.Appoinment;
using CuidandoPawsApi.Domain.Utils;
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

        public async Task<ResultT<IEnumerable<ServiceCatalogDTos>>> CheckAvailabilityAsync(int serviceId, CancellationToken cancellationToken)
        {
            var serviceCatalog = await _serviceCatalogRepository.GetByIdAsync(serviceId,cancellationToken);

            if (serviceCatalog == null)
            {
                return ResultT<IEnumerable<ServiceCatalogDTos>>.Failure(Error.NotFound("404", "Id not found"));
            }
            serviceCatalog.IsAvaible = true;

            await _serviceCatalogRepository.SaveAsync();

            var availableServiceCatalog =  await _appoinmentRepository.CheckAvailabilityAsync(serviceCatalog.Id,cancellationToken);
                
            var serviceCatalogDto = _mapper.Map<IEnumerable<ServiceCatalogDTos>>(availableServiceCatalog);

            return ResultT<IEnumerable<ServiceCatalogDTos>>.Success(serviceCatalogDto);
        }
    }
}
