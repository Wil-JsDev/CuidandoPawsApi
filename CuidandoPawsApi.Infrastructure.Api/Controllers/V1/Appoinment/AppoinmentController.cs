using Asp.Versioning;
using CuidandoPawsApi.Application.DTOs.Appoinment;
using CuidandoPawsApi.Application.DTOs.ServiceCatalog;
using CuidandoPawsApi.Domain.Enum;
using CuidandoPawsApi.Domain.Ports.UseCase.Appoinment;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

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
        private readonly IValidator<CreateUpdateAppoinmentDTos> _validator;

        public AppoinmentController(ICreateAppoinment<CreateUpdateAppoinmentDTos, AppoinmentDTos> createAppoinment, IGetAppoinment<AppoinmentDTos> getAppoinment, 
            IGetByIdAppoinment<AppoinmentDTos> findByIdAppoinment, IUpdateAppoinment<CreateUpdateAppoinmentDTos, AppoinmentDTos> updateAppoinment, IGetAppoinmentLastAddedOndate<AppoinmentDTos> getAppoinmentLastAdded, 
            ICheckAppoinmentAvailability<ServiceCatalogDTos> checkAppoinmentAvailability, IValidator<CreateUpdateAppoinmentDTos> validator ,
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
            _validator = validator;
        }

        [HttpPost]
        [EnableRateLimiting("fixed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAppoinmentAsync([FromBody] CreateUpdateAppoinmentDTos dTos, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(dTos, cancellationToken);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var appoinmentNew = await _createAppoinment.AddAsync(dTos, cancellationToken);

            if (appoinmentNew.IsSuccess)
            {
                return Ok(appoinmentNew.Value);
            }
            
            return BadRequest(appoinmentNew.Error!);

        }

        [HttpGet("{id}")]
        [EnableRateLimiting("fixed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAppoinmentAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var appoinmentId = await _findByIdAppoinment.GetByIdAsync(id, cancellationToken);
            if (appoinmentId.IsSuccess)
            {
                return Ok(appoinmentId.Value);
            }

            return NotFound(appoinmentId.Error);
        }

        [HttpGet]
        [EnableRateLimiting("fixed")]
        public async Task<IEnumerable<AppoinmentDTos>> AppoinmentAllAsync(CancellationToken cancellationToken)
        {
            return await _getAppoinment.GetAllAsync(cancellationToken);     
        }

        [HttpDelete("{appoinmentId}")]
        [EnableRateLimiting("fixed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAppoinmentAsync([FromRoute] int appoinmentId, CancellationToken cancellationToken)
        {
            var appoinment = await _deleteAppoinment.DeleteAppoinmentAsync(appoinmentId,cancellationToken);
            if (appoinment.IsSuccess)
            {
                return NoContent();
            }

            return NotFound(appoinment.Error);
        }

        [HttpPatch("{appoinmentId}")]
        [DisableRateLimiting]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAppoinmentAsync([FromRoute] int appoinmentId, [FromBody] CreateUpdateAppoinmentDTos dTos ,CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(dTos, cancellationToken);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var appoinment = await _findByIdAppoinment.GetByIdAsync(appoinmentId, cancellationToken);
            if (appoinment.IsSuccess)
            {
                var appoinmnentNew = await _updateAppoinment.UpdateAsync(appoinmentId,dTos,cancellationToken);
                return Ok(appoinmnentNew.Value);
            }
            return NotFound(appoinment.Error);
        }

        [HttpGet("check-availability/service-catalog/{serviceId}")]
        [EnableRateLimiting("fixed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckAppoinmentAvailabilityAsync([FromRoute] int serviceId ,CancellationToken cancellationToken)
        {
            var serviceCatalog = await _checkAppoinmentAvailability.CheckAvailabilityAsync(serviceId,cancellationToken);
            if (serviceCatalog.IsSuccess)
            {
                return Ok(serviceCatalog.Value);
            }
            return NotFound(serviceCatalog.Error);
        }
 
        [HttpGet("availability-service/service-catalog/{serviceCatalogId}")]
        [EnableRateLimiting("fixed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AvailabilityServiceAsync([FromRoute] int serviceCatalogId, CancellationToken cancellationToken)
        {
            var serviceCatalogAvailability = await _getAppoinmentAvailabilityService.GetAvailabilityServiceAsync(serviceCatalogId,cancellationToken);
            if (serviceCatalogAvailability.IsSuccess)
            {   
                return Ok(serviceCatalogAvailability.Value);
            }

            return NotFound(serviceCatalogAvailability.Error);
        }

        [HttpGet("last-added")]
        [EnableRateLimiting("fixed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> LastAddedAppoinmentAsync([FromQuery] FilterDate filterDate, CancellationToken cancellationToken)
        {
            var appoinment = await _getAppoinmentLastAdded.GetLastAddedOnDateAsync(filterDate,cancellationToken);
            if (appoinment.IsSuccess)
            {
                return Ok(appoinment.Value);
            }
            return BadRequest(appoinment.Error);
        }
    }
}
