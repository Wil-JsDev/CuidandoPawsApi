using Asp.Versioning;
using CuidandoPawsApi.Application.DTOs.Pets;
using CuidandoPawsApi.Domain.Pagination;
using CuidandoPawsApi.Domain.Ports.UseCase;
using CuidandoPawsApi.Domain.Ports.UseCase.Pets;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CuidandoPawsApi.Infrastructure.Api.Controllers.V1.Pets
{
    
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{vresion:ApiVersion}/pets")]
    public class PetsController : ControllerBase
    {

        private readonly ICreatePets<CreatePetsDTos, PetsDTos> _createPets;
        private readonly IGetPagedPets<PetsDTos> _getPagedPets;
        private readonly IGetByIdPets<PetsDTos> _getByIdPets;
        private readonly IDeletePets<PetsDTos> _deletePets;
        private readonly IUpdatePets<UpdatePetsDTos, PetsDTos> _updatePets;
        private readonly IGetPetsLastAddedOfDay<PetsDTos> _getPetsLastAddedOfDay;
        private readonly IValidator<CreatePetsDTos> _createValidator;
        private readonly IValidator<UpdatePetsDTos> _updateValidator;

        public PetsController(ICreatePets<CreatePetsDTos, PetsDTos> createPets, IGetPagedPets<PetsDTos> getPagedPets, IGetByIdPets<PetsDTos> getByIdPets, IDeletePets<PetsDTos> deletePets,
            IUpdatePets<UpdatePetsDTos, PetsDTos> updatePets, IGetPetsLastAddedOfDay<PetsDTos> getPetsLastAddedOfDay, 
            IValidator<CreatePetsDTos> createValidator, IValidator<UpdatePetsDTos> updateValidator)
        {
            _createPets = createPets;
            _getPagedPets = getPagedPets;
            _getByIdPets = getByIdPets;
            _deletePets = deletePets;
            _updatePets = updatePets;
            _getPetsLastAddedOfDay = getPetsLastAddedOfDay;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpPost]
        [DisableRateLimiting]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePetsAsync([FromBody] CreatePetsDTos createPetsDTos, CancellationToken cancellationToken)
        {
            var result = await _createValidator.ValidateAsync(createPetsDTos, cancellationToken);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var resultPets = await _createPets.AddAsync(createPetsDTos, cancellationToken);
            if (resultPets.IsSuccess)
            {
                return Ok(resultPets.Value);
            }

            return BadRequest(resultPets.Error);

        }

        [HttpDelete("{id}")]
        [DisableRateLimiting]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PetsDTos>> DeletePetsAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _deletePets.DeleteAsync(id,cancellationToken);
            if (result.IsSuccess)
            {
                return NoContent();
            }

            return NotFound(result.Error);
        }

        [HttpGet("{id}")]
        [EnableRateLimiting("fixed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdPetsAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _getByIdPets.GetByIdAsync(id,cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Error);
        }

        [HttpPut("{id}")]
        [EnableRateLimiting("fixed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PetsDTos>> UpdatePetsAsync([FromRoute] int id, [FromBody] UpdatePetsDTos updatePetsDTos, CancellationToken cancellationToken )
        {

            var result = await _updateValidator.ValidateAsync(updatePetsDTos, cancellationToken);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var resultPets = await _getByIdPets.GetByIdAsync(id,cancellationToken);
            if (resultPets.IsSuccess)
            {
                var petNew = await _updatePets.UpdateAsync(id,updatePetsDTos,cancellationToken);
                return Ok(petNew.Value);
            }
            return NotFound(resultPets.Error);

        }

        [HttpGet("last-added-today")]
        [EnableRateLimiting("fixed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PetsDTos>> GetLastAddedPetsAsync(CancellationToken cancellationToken)
        {
            var pets = await _getPetsLastAddedOfDay.GetLastAddedPetsOfDayAsync(cancellationToken);
            return Ok(pets);
        }

        [HttpGet("pagination")]
        [EnableRateLimiting("fixed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPagedPetsAsync([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            var result =  await _getPagedPets.ListWithPaginationAsync(pageNumber,pageSize,cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }

    }
}
