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
    public class CreateServiceCatalog : ICreateServiceCatalog<ServiceCatalogDTos, CreateServiceCatalogDTos>
    {

        private readonly IServiceCatalogRepository _serviceCatalogRepository;
        private readonly IMapper _mapper;

        public CreateServiceCatalog(IServiceCatalogRepository serviceCatalogRepository, IMapper mapper)
        {
            _serviceCatalogRepository = serviceCatalogRepository;
            _mapper = mapper;
        }

        public async Task <ResultT<ServiceCatalogDTos>> CreateAsync(CreateServiceCatalogDTos dto, CancellationToken cancellation)
        {
            var serviceCatalog = _mapper.Map<ServiceCatalog>(dto);

            if (serviceCatalog != null)
            {
                await _serviceCatalogRepository.AddAsync(serviceCatalog,cancellation);
                var serviceCatalogDto = _mapper.Map<ServiceCatalogDTos>(serviceCatalog);
                return ResultT<ServiceCatalogDTos>.Success(serviceCatalogDto);
            }
            return ResultT<ServiceCatalogDTos>.Failure(Error.Failure("400", "Error entering data"));
        }
    }
}
