using AutoMapper;
using CuidandoPawsApi.Application.DTOs.ServiceCatalog;
using CuidandoPawsApi.Domain.Models;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Domain.Ports.UseCase.ServiceCatalog;
using CuidandoPawsApi.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.Adapters.ServiceCatalogAdapt
{
    public class GetByIdServiceCatalog : IGetByIdServiceCatalog<ServiceCatalogDTos>
    {
        private readonly IServiceCatalogRepository _serviceCatalogRepository;
        private readonly IMapper _mapper;

        public GetByIdServiceCatalog(IServiceCatalogRepository serviceCatalogRepository, IMapper mapper)
        {
            _serviceCatalogRepository = serviceCatalogRepository;
            _mapper = mapper;
        }

        public async Task <ResultT<ServiceCatalogDTos>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var serviceCatalogId = await _serviceCatalogRepository.GetByIdAsync(id, cancellationToken);

            if (serviceCatalogId != null)
            {
                var serviceCatalogDto = _mapper.Map<ServiceCatalogDTos>(serviceCatalogId);

                return ResultT<ServiceCatalogDTos>.Success(serviceCatalogDto);
            }
            return ResultT<ServiceCatalogDTos>.Failure(Error.NotFound("404", "id not found"));
        }
    }
}
