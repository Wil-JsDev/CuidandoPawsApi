using Asp.Versioning;
using CuidandoPawsApi.Application.Common;
using CuidandoPawsApi.Application.DTOs.ServiceCatalog;
using CuidandoPawsApi.Domain.Ports.UseCase.ServiceCatalog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CuidandoPawsApi.Infrastructure.Api.Controllers.V1.ServiceCatalog
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/service-catalog")]
    public class ServiceCatalogController : ControllerBase
    {
        private readonly ICreateServiceCatalog<ServiceCatalogDTos, CreateServiceCatalogDTos> _createServiceCatalog;
        private readonly IGetServiceCatalog<ServiceCatalogDTos> _getServiceCatalog;
        private readonly IGetByIdServiceCatalog<ServiceCatalogDTos> _getByIdServiceCatalog;
        private readonly IUpdateServiceCatalog<ServiceCatalogDTos, UpdateServiceCatalogDTos> _updateServiceCatalog;
        private readonly IDeleteServiceCatalog<ServiceCatalogDTos> _deleteServiceCatalog;

        public ServiceCatalogController(ICreateServiceCatalog<ServiceCatalogDTos,CreateServiceCatalogDTos> createServiceCatalog, IGetServiceCatalog<ServiceCatalogDTos> getServiceCatalog, 
            IGetByIdServiceCatalog<ServiceCatalogDTos> getByIdServiceCatalog, IUpdateServiceCatalog<ServiceCatalogDTos, UpdateServiceCatalogDTos> updateServiceCatalog,
            IDeleteServiceCatalog<ServiceCatalogDTos> deleteServiceCatalog)
        {
            _createServiceCatalog = createServiceCatalog;
            _getServiceCatalog = getServiceCatalog;
            _getByIdServiceCatalog = getByIdServiceCatalog;
            _updateServiceCatalog = updateServiceCatalog;
            _deleteServiceCatalog = deleteServiceCatalog;
        }


        [HttpGet("all")]
        public async Task<ActionResult<ServiceCatalogDTos>> GetServiceCatalogAsync(CancellationToken cancellationToken) =>
            Ok(await _getServiceCatalog.GetAllAsync(cancellationToken));
        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdServiceCatalogAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _getByIdServiceCatalog.GetByIdAsync(id,cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return NotFound(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> CreateServiceCatalogAsync(CreateServiceCatalogDTos catalogDTos, CancellationToken cancellationToken)
        {
            var result = await _createServiceCatalog.CreateAsync(catalogDTos,cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServicerCatalogAsync([FromRoute] int id, UpdateServiceCatalogDTos catalogDTos, CancellationToken cancellationToken)
        {
            var result = await _getByIdServiceCatalog.GetByIdAsync(id,cancellationToken);
            if (result.IsSuccess)
            {
                var serviceCatalog = await _updateServiceCatalog.UpdateAsync(id,catalogDTos,cancellationToken);
                return Ok(result.Value);
            }
                
                return NotFound(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceCatalogAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _deleteServiceCatalog.DeleteAsync(id,cancellationToken);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result.Error);
        }
    }
}
