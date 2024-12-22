using AutoMapper;
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
    public class GetAppoinmentAvailabilityService : IGetAppoinmentAvailabilityService<ServiceCatalogDTos>
    {

        private readonly IAppoinmentRepository _appoinmentRepository;
        private readonly IMapper _mapper;
        private readonly IServiceCatalogRepository _serviceCatalogRepository;

        public GetAppoinmentAvailabilityService(IAppoinmentRepository appoinmentRepository, IMapper mapper, IServiceCatalogRepository serviceCatalogRepository)
        {
            _appoinmentRepository = appoinmentRepository;
            _mapper = mapper;
            _serviceCatalogRepository = serviceCatalogRepository;
        }

        public async Task <ResultT<IEnumerable<ServiceCatalogDTos>>> GetAvailabilityServiceAsync(int serviceCatalog, CancellationToken cancellationToken)
        {
            var serviceCatalogId = await _serviceCatalogRepository.GetByIdAsync(serviceCatalog,cancellationToken);

            if (serviceCatalogId == null)
            {
                return ResultT<IEnumerable<ServiceCatalogDTos>>.Failure(Error.NotFound("404", "Id not found"));
            }

            
            bool isActive = true;
            var availableServiceCatalog = await _appoinmentRepository.GetAvailabilityServiceAsync(serviceCatalogId,isActive,cancellationToken);

            var serviceCatalogDto = _mapper.Map<IEnumerable<ServiceCatalogDTos>>(availableServiceCatalog);
            
            return ResultT<IEnumerable<ServiceCatalogDTos>>.Success(serviceCatalogDto);
        }
    }
}
