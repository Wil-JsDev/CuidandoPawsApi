﻿using Asp.Versioning;
using CuidandoPawsApi.Application.Common;
using CuidandoPawsApi.Application.DTOs.Species;
using CuidandoPawsApi.Domain.Ports.UseCase.Species;
using CuidandoPawsApi.Domain.Utils;
using FluentValidation;
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
        private readonly IValidator<CreateUpdateSpecieDTos> _validator;

        public SpeciesController(ICreateSpecies<CreateUpdateSpecieDTos, SpeciesDTos> createSpecies, IGetSpecies<SpeciesDTos> getSpecies, IGetByIdSpecies<SpeciesDTos> getByIdSpecies, 
            IUpdateSpecies<CreateUpdateSpecieDTos, SpeciesDTos> updateSpecies,  IGetSpeciesLastAdded<SpeciesDTos> getSpeciesLastAdded, 
            IGetSpeciesOrderById<SpeciesDTos> getOrderById, IDeleteSpecies<SpeciesDTos> deleteSpecies, IValidator<CreateUpdateSpecieDTos> validator)
        {
            _createSpecies = createSpecies;
            _getSpecies = getSpecies;
            _getByIdSpecies = getByIdSpecies;
            _updateSpecies = updateSpecies;
            _getSpeciesLastAdded = getSpeciesLastAdded;
            _getOrderById = getOrderById;
            _deleteSpecies = deleteSpecies;
            _validator = validator;
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
        public async Task<IActionResult> GetByIdSpeciesAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _getByIdSpecies.GetById(id, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Error);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSpeciesAsync(CreateUpdateSpecieDTos specieDTos, CancellationToken cancellationToken)
        {

            var result = await _validator.ValidateAsync(specieDTos, cancellationToken);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var resultSpecies = await _createSpecies.AddAsync(specieDTos, cancellationToken);
            if (resultSpecies.IsSuccess)
            {
                return Ok(resultSpecies.Value);
            }
            return BadRequest(resultSpecies.Error);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateSpeciesAsync([FromRoute] int id,[FromBody] CreateUpdateSpecieDTos speciesDto, CancellationToken cancellationToken )
        {

            var result = await _validator.ValidateAsync(speciesDto,cancellationToken);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var resultSpecies = await _getByIdSpecies.GetById(id,cancellationToken);
            if (resultSpecies.IsSuccess)
            {
                var speciesUpdate = await _updateSpecies.UpdateAsync(id,speciesDto,cancellationToken);
                return Ok(speciesUpdate.Value);
            }

            return NotFound(resultSpecies.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpeciesAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _deleteSpecies.DeleteSpeciesAsync(id,cancellationToken);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return NotFound(result.Error);
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
        public async Task<IActionResult> GetOrderedByISpeciesAsync([FromQuery] string sort, [FromQuery] string direction, CancellationToken cancellationToken)
        {
            var result = await _getOrderById.GetOrderedByIdAsync(sort,direction,cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }

    }
}
