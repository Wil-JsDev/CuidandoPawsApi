using Asp.Versioning;
using CuidandoPawsApi.Application.Common;
using CuidandoPawsApi.Application.DTOs.Species;
using CuidandoPawsApi.Domain.Ports.UseCase.Species;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CuidandoPawsApi.Infrastructure.Api.Controllers.V1.Species
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/species")]
    public class SpeciesController : ControllerBase
    {

        private readonly ICreateSpecies<CreateUpdateSpecieDTos, SpeciesDTos> _createSpecies;
        private readonly IGetSpecies<SpeciesDTos> _getSpecies;
        private readonly IGetByIdSpecies<SpeciesDTos> _getByIdSpecies;
        private readonly IUpdateSpecies<CreateUpdateSpecieDTos, SpeciesDTos> _updateSpecies;
        private readonly IGetSpeciesLastAdded<SpeciesDTos> _getSpeciesLastAdded;
        private readonly IGetSpeciesOrderById<SpeciesDTos> _getOrderById;
        private readonly IDeleteSpecies<SpeciesDTos> _deleteSpecies;

        public SpeciesController(ICreateSpecies<CreateUpdateSpecieDTos, SpeciesDTos> createSpecies, IGetSpecies<SpeciesDTos> getSpecies, IGetByIdSpecies<SpeciesDTos> getByIdSpecies, 
            IUpdateSpecies<CreateUpdateSpecieDTos, SpeciesDTos> updateSpecies,  IGetSpeciesLastAdded<SpeciesDTos> getSpeciesLastAdded, 
            IGetSpeciesOrderById<SpeciesDTos> getOrderById, IDeleteSpecies<SpeciesDTos> deleteSpecies)
        {
            _createSpecies = createSpecies;
            _getSpecies = getSpecies;
            _getByIdSpecies = getByIdSpecies;
            _updateSpecies = updateSpecies;
            _getSpeciesLastAdded = getSpeciesLastAdded;
            _getOrderById = getOrderById;
            _deleteSpecies = deleteSpecies;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SpeciesDTos>> GetSpeciesAsyn(CancellationToken cancellationToken)
        {
            var speciesAll = await _getSpecies.GetAllAsync(cancellationToken);
            return Ok(speciesAll);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SpeciesDTos>> GetByIdSpeciesAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var speciesId = await _getByIdSpecies.GetById(id, cancellationToken);
            if (speciesId != null)
            {
                return Ok(ApiResponse<SpeciesDTos>.SuccessResponse(speciesId));
            }
            return NotFound(ApiResponse<string>.ErrorResponse("Id not found"));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SpeciesDTos>> CreateSpeciesAsync(CreateUpdateSpecieDTos specieDTos, CancellationToken cancellationToken)
        {
            var speciesNew = await _createSpecies.AddAsync(specieDTos, cancellationToken);
            if (speciesNew != null)
            {
                return Ok(ApiResponse<SpeciesDTos>.SuccessResponse(speciesNew));
            }

            return BadRequest(ApiResponse<string>.ErrorResponse("There were problems with the data entered"));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<SpeciesDTos>> UpdateSpeciesAsync([FromRoute] int id,[FromBody] CreateUpdateSpecieDTos speciesDto, CancellationToken cancellationToken )
        {
            var speciesId = await _getByIdSpecies.GetById(id,cancellationToken);
            if (speciesId != null)
            {
                var speciesUpdate = await _updateSpecies.UpdateAsync(id,speciesDto,cancellationToken);
                return Ok(ApiResponse<SpeciesDTos>.SuccessResponse(speciesUpdate));
            }

            return NotFound(ApiResponse<SpeciesDTos>.ErrorResponse("There were problems with the data entered"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SpeciesDTos>> DeleteSpeciesAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var speciesId = await _deleteSpecies.DeleteSpeciesAsync(id,cancellationToken);
            if (speciesId != null)
            {
                return NoContent();
            }
            return NotFound(ApiResponse<string>.ErrorResponse("Id not found"));
        }

        [HttpGet("last-added")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SpeciesDTos>> GetLastAddedSpeciesAsync(CancellationToken cancellationToken)
        {
            var specieLastAdded = await _getSpeciesLastAdded.GetLastAddedAsync(cancellationToken);
            return Ok(specieLastAdded);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SpeciesDTos>> GetOrderedByISpeciesAsync([FromQuery] string sort, [FromQuery] string direction, CancellationToken cancellationToken)
        {
            var speciesById = await _getOrderById.GetOrderedByIdAsync(direction,cancellationToken);
            return Ok(speciesById);
        }

    }
}
