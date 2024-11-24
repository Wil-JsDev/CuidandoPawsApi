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
    public class DeleteServiceCatalog : IDeleteServiceCatalog<ServiceCatalogDTos>
    {
        private readonly IServiceCatalogRepository _serviceCatalogRepository;
        private readonly IMapper _mapper;

        public DeleteServiceCatalog(IServiceCatalogRepository serviceCatalogRepository, IMapper mapper)
        {
            _serviceCatalogRepository = serviceCatalogRepository;
            _mapper = mapper;
        }

        public async Task<ServiceCatalogDTos> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var serviceCatalogId = await _serviceCatalogRepository.GetByIdAsync(id,cancellationToken);

            if (serviceCatalogId != null)
            {
                await _serviceCatalogRepository.DeleteAsync(serviceCatalogId,cancellationToken);
                var serviceCatalogDto = _mapper.Map<ServiceCatalogDTos>(serviceCatalogId);
                return serviceCatalogDto;
            }
            return null;
        }
    }
}
