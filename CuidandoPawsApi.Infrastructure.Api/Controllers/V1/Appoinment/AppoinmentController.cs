using Asp.Versioning;
using CuidandoPawsApi.Application.Common;
using CuidandoPawsApi.Application.DTOs.Appoinment;
using CuidandoPawsApi.Application.DTOs.ServiceCatalog;
using CuidandoPawsApi.Domain.Enum;
using CuidandoPawsApi.Domain.Ports.UseCase.Appoinment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CuidandoPawsApi.Infrastructure.Api.Controllers.V1.Appoinment
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/appoinment")]
    public class AppoinmentController : ControllerBase
    {
        private readonly ICreateAppoinment<CreateUpdateAppoinmentDTos, AppoinmentDTos> _createAppoinment;
        private readonly IGetAppoinment<AppoinmentDTos> _getAppoinment;
        private readonly IGetByIdAppoinment<AppoinmentDTos> _findByIdAppoinment;
        private readonly IUpdateAppoinment<CreateUpdateAppoinmentDTos, AppoinmentDTos> _updateAppoinment;
        private readonly IGetAppoinmentLastAddedOndate<AppoinmentDTos> _getAppoinmentLastAdded;
        private readonly ICheckAppoinmentAvailability<ServiceCatalogDTos> _checkAppoinmentAvailability;
        private readonly IGetAppoinmentAvailabilityService<ServiceCatalogDTos> _getAppoinmentAvailabilityService;
        private readonly IDeleteAppoinment<AppoinmentDTos> _deleteAppoinment;

        public AppoinmentController(ICreateAppoinment<CreateUpdateAppoinmentDTos, AppoinmentDTos> createAppoinment, IGetAppoinment<AppoinmentDTos> getAppoinment, 
            IGetByIdAppoinment<AppoinmentDTos> findByIdAppoinment, IUpdateAppoinment<CreateUpdateAppoinmentDTos, AppoinmentDTos> updateAppoinment, IGetAppoinmentLastAddedOndate<AppoinmentDTos> getAppoinmentLastAdded, 
            ICheckAppoinmentAvailability<ServiceCatalogDTos> checkAppoinmentAvailability, 
            IGetAppoinmentAvailabilityService<ServiceCatalogDTos> getAppoinmentAvailabilityService, IDeleteAppoinment<AppoinmentDTos> deleteAppoinment)
        {
            _createAppoinment = createAppoinment;
            _getAppoinment = getAppoinment;
            _findByIdAppoinment = findByIdAppoinment;
            _updateAppoinment = updateAppoinment;
            _getAppoinmentLastAdded = getAppoinmentLastAdded;
            _checkAppoinmentAvailability = checkAppoinmentAvailability;
            _getAppoinmentAvailabilityService = getAppoinmentAvailabilityService;
            _deleteAppoinment = deleteAppoinment;
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AppoinmentDTos>> CreateAppoinmentAsync([FromBody] CreateUpdateAppoinmentDTos dTos, CancellationToken cancellationToken)
        {
            var appoinmentNew = await _createAppoinment.AddAsync(dTos,cancellationToken);
            if (appoinmentNew != null)
            {
                return Ok(ApiResponse<AppoinmentDTos>.SuccessResponse(appoinmentNew));
            }
            return BadRequest(ApiResponse<string>.ErrorResponse("Error entering data"));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AppoinmentDTos>> GetByIdAppoinmentAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var appoinmentId = await _findByIdAppoinment.GetByIdAsync(id, cancellationToken);
            if (appoinmentId != null)
            {
                return Ok(ApiResponse<AppoinmentDTos>.SuccessResponse(appoinmentId));
            }
            return NotFound(ApiResponse<string>.ErrorResponse("Id not found"));
        }

        [HttpGet("all")]
        public async Task<IEnumerable<AppoinmentDTos>> AppoinmentAllAsync(CancellationToken cancellationToken)
        {
            var appoinments = await _getAppoinment.GetAllAsync(cancellationToken);
            return appoinments;
        }

        [HttpDelete("{appoinmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAppoinmentAsync([FromRoute] int appoinmentId, CancellationToken cancellationToken)
        {
            var appoinment = await _deleteAppoinment.DeleteAppoinmentAsync(appoinmentId,cancellationToken);
            if (appoinment != null)
            {
                return NoContent();
            }

            return NotFound(ApiResponse<string>.ErrorResponse("Id not found"));
        }

        [HttpPatch("{appoinmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AppoinmentDTos>> UpdateAppoinmentAsync([FromRoute] int appoinmentId, [FromBody] CreateUpdateAppoinmentDTos dTos ,CancellationToken cancellationToken)
        {
            var appoinment = await _findByIdAppoinment.GetByIdAsync(appoinmentId, cancellationToken);
            if (appoinment != null)
            {
                var appoinmnentNew = await _updateAppoinment.UpdateAsync(appoinmentId,dTos,cancellationToken);
                return Ok(ApiResponse<AppoinmentDTos>.SuccessResponse(appoinmnentNew));
            }
            return NotFound(ApiResponse<string>.ErrorResponse("Id not found"));
        }

        [HttpGet("check-availability/service-catalog/{serviceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<ServiceCatalogDTos>> CheckAppoinmentAvailabilityAsync([FromRoute] int serviceId ,CancellationToken cancellationToken)
        {
            var serviceCatalog = await _checkAppoinmentAvailability.CheckAvailabilityAsync(serviceId,cancellationToken);
            return serviceCatalog;
        }
 
        [HttpGet("availability-service/service-catalog/{serviceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<ServiceCatalogDTos>> AvailabilityServiceAsync([FromRoute] int serviceCatalogId, CancellationToken cancellationToken)
        {
            var serviceCatalogAvailability = await _getAppoinmentAvailabilityService.GetAvailabilityServiceAsync(serviceCatalogId,cancellationToken);
            return serviceCatalogAvailability;
        }

        [HttpGet("last-added")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AppoinmentDTos>> LastAddedAppoinmentAsync([FromQuery] FilterDate filterDate, CancellationToken cancellationToken)
        {
            var appoinment = await _getAppoinmentLastAdded.GetLastAddedOnDateAsync(filterDate,cancellationToken);
            return Ok(appoinment);
        }
    }
}
