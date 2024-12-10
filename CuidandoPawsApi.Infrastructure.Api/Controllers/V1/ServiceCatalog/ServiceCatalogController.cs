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
        public async Task<ActionResult<ServiceCatalogDTos>> GetServiceCatalogAsync(CancellationToken cancellationToken)
        {
            var serviceCatalog = await _getServiceCatalog.GetAllAsync(cancellationToken);
            return Ok(serviceCatalog);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceCatalogDTos>> GetByIdServiceCatalogAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var serviceCatalog = await _getByIdServiceCatalog.GetByIdAsync(id,cancellationToken);
            if (serviceCatalog != null)
            {
                Ok(ApiResponse<ServiceCatalogDTos>.SuccessResponse(serviceCatalog));
            }

            return NotFound(ApiResponse<ServiceCatalogDTos>.ErrorResponse("Id not found"));
        }

        [HttpPost("add")]
        public async Task<ActionResult<ServiceCatalogDTos>> CreateServiceCatalogAsync(CreateServiceCatalogDTos catalogDTos, CancellationToken cancellationToken)
        {
            var serviceCatalogNew = await _createServiceCatalog.CreateAsync(catalogDTos,cancellationToken);
            if (serviceCatalogNew != null)
            {
                return Ok(ApiResponse<ServiceCatalogDTos>.SuccessResponse(serviceCatalogNew));
            }

            return BadRequest(ApiResponse<ServiceCatalogDTos>.ErrorResponse("Error entering data"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceCatalogDTos>> UpdateServicerCatalogAsync([FromRoute] int id, UpdateServiceCatalogDTos catalogDTos, CancellationToken cancellationToken)
        {
            var serviceCatalogId = await _getByIdServiceCatalog.GetByIdAsync(id,cancellationToken);
            if (serviceCatalogId != null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse("Id not found"));
            }
                
            var serviceCatalog = await _updateServiceCatalog.UpdateAsync(id,catalogDTos,cancellationToken);
            return Ok(ApiResponse<ServiceCatalogDTos>.SuccessResponse(serviceCatalog));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceCatalogDTos>> DeleteServiceCatalogAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var serviceCatalog = _deleteServiceCatalog.DeleteAsync(id,cancellationToken);
            if (serviceCatalog != null)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse("Id not found"));
            }
            return NoContent();
        }
    }
}
