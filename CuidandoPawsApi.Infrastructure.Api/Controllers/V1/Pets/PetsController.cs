using Asp.Versioning;
using CuidandoPawsApi.Application.Common;
using CuidandoPawsApi.Application.DTOs.Pets;
using CuidandoPawsApi.Domain.Pagination;
using CuidandoPawsApi.Domain.Ports.UseCase;
using CuidandoPawsApi.Domain.Ports.UseCase.Pets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public PetsController(ICreatePets<CreatePetsDTos, PetsDTos> createPets, IGetPagedPets<PetsDTos> getPagedPets, IGetByIdPets<PetsDTos> getByIdPets, IDeletePets<PetsDTos> deletePets,
            IUpdatePets<UpdatePetsDTos, PetsDTos> updatePets, IGetPetsLastAddedOfDay<PetsDTos> getPetsLastAddedOfDay)
        {
            _createPets = createPets;
            _getPagedPets = getPagedPets;
            _getByIdPets = getByIdPets;
            _deletePets = deletePets;
            _updatePets = updatePets;
            _getPetsLastAddedOfDay = getPetsLastAddedOfDay;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PetsDTos>> CreatePetsAsync([FromBody] CreatePetsDTos createPetsD, CancellationToken cancellationToken)
        {
            var petsNew = await _createPets.AddAsync(createPetsD, cancellationToken);
            if (petsNew != null)
            {
                return Ok(ApiResponse<PetsDTos>.SuccessResponse(petsNew));
            }

            return BadRequest(ApiResponse<string>.ErrorResponse("Error entering data"));

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PetsDTos>> DeletePetsAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var pets = await _deletePets.DeleteAsync(id,cancellationToken);
            if (pets != null)
            {
                return NoContent();
            }

            return NotFound(ApiResponse<string>.ErrorResponse("id not found"));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PetsDTos>> GetByIdPetsAsync(int id, CancellationToken cancellationToken)
        {
            var petsId = await _getByIdPets.GetByIdAsync(id,cancellationToken);
            if (petsId != null)
            {
                return Ok(ApiResponse<PetsDTos>.SuccessResponse(petsId));
            }
            return NotFound(ApiResponse<string>.ErrorResponse("id not found"));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PetsDTos>> UpdatePetsAsync([FromRoute] int id, [FromBody] UpdatePetsDTos updatePetsDTos, CancellationToken cancellationToken )
        {
            var petId = await _getByIdPets.GetByIdAsync(id,cancellationToken);
            if (petId != null)
            {
                var petNew = await _updatePets.UpdateAsync(id,updatePetsDTos,cancellationToken);
                return Ok(ApiResponse<PetsDTos>.SuccessResponse(petNew));
            }

            return NotFound(ApiResponse<string>.ErrorResponse("id not found"));

        }

        [HttpGet("last-added-today")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PetsDTos>> GetLastAddedPetsAsync(CancellationToken cancellationToken)
        {
            var pets = await _getPetsLastAddedOfDay.GetLastAddedPetsOfDayAsync(cancellationToken);
            return Ok(pets);
        }

        [HttpGet("pagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PetsDTos>> GetPagedPetsAsync([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            var pets =  await _getPagedPets.ListWithPaginationAsync(pageNumber,pageSize,cancellationToken);
            return Ok(pets);
        }

    }
}
