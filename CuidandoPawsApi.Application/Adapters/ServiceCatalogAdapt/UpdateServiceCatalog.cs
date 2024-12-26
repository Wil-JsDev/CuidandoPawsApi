using AutoMapper;
using CuidandoPawsApi.Application.DTOs.ServiceCatalog;
using CuidandoPawsApi.Domain.Models;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Domain.Ports.UseCase.Appoinment;
using CuidandoPawsApi.Domain.Ports.UseCase.ServiceCatalog;
using CuidandoPawsApi.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.Adapters.ServiceCatalogAdapt
{
    public class UpdateServiceCatalog : IUpdateServiceCatalog<ServiceCatalogDTos, UpdateServiceCatalogDTos>
    {

        private readonly IServiceCatalogRepository _serviceCatalogRepository;
        private readonly IMapper _mapper;

        public UpdateServiceCatalog(IServiceCatalogRepository serviceCatalogRepository, IMapper mapper)
        {
            _serviceCatalogRepository = serviceCatalogRepository;
            _mapper = mapper;
        }

        public async Task <ResultT<ServiceCatalogDTos>> UpdateAsync(int id,UpdateServiceCatalogDTos dto, CancellationToken cancellationToken)
        {
            var serviceCatalog = await _serviceCatalogRepository.GetByIdAsync(id,cancellationToken);

            if (serviceCatalog != null)
            {
                serviceCatalog = _mapper.Map<UpdateServiceCatalogDTos, ServiceCatalog>(dto,serviceCatalog);
                await _serviceCatalogRepository.UpdateAsync(serviceCatalog);
                var serviceCatalogDto = _mapper.Map<ServiceCatalogDTos>(serviceCatalog);
                return ResultT<ServiceCatalogDTos>.Success(serviceCatalogDto);
            }

            return ResultT<ServiceCatalogDTos>.Failure(Error.NotFound("404", "id not found"));
        }
    }
}
