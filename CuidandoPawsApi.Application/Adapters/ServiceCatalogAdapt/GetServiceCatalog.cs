using AutoMapper;
using CuidandoPawsApi.Application.DTOs.ServiceCatalog;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Domain.Ports.UseCase.ServiceCatalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.Adapters.ServiceCatalogAdapt
{
    public class GetServiceCatalog : IGetServiceCatalog<ServiceCatalogDTos>
    {
        private readonly IServiceCatalogRepository _serviceCatalogRepository;
        private readonly IMapper _mapper;

        public GetServiceCatalog(IServiceCatalogRepository serviceCatalogRepository, IMapper mapper)
        {
            _serviceCatalogRepository = serviceCatalogRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceCatalogDTos>> GetAllAsync(CancellationToken cancellationToken)
        {
            var serviceCatalogs = await _serviceCatalogRepository.GetAllAsync(cancellationToken);
            return serviceCatalogs.Select(x => _mapper.Map<ServiceCatalogDTos>(x));
        }
    }
}
