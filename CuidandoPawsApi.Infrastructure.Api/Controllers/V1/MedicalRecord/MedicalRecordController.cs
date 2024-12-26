using Asp.Versioning;
using CuidandoPawsApi.Application.Adapters.PetsAdapt;
using CuidandoPawsApi.Application.Common;
using CuidandoPawsApi.Application.DTOs.MedicalRecord;
using CuidandoPawsApi.Application.DTOs.Pets;
using CuidandoPawsApi.Domain.Ports.UseCase;
using CuidandoPawsApi.Domain.Ports.UseCase.MedicalRecord;
using CuidandoPawsApi.Domain.Utils;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CuidandoPawsApi.Infrastructure.Api.Controllers.V1.Pets
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/medical-record")]
    public class MedicalRecordController : ControllerBase
    {
        private readonly ICreateMedicalRecord<CreateUpdateMedicalRecordDTos, MedicalRecordDTos> _createMedicalRecord;
        private readonly IGetByIdMedicalRecord<MedicalRecordDTos> _getByIdMedicalRecord;
        private readonly IUpdateMedicalRecord<CreateUpdateMedicalRecordDTos, MedicalRecordDTos> _updateMedicalRecord;
        private readonly IGetMedicalRecord<MedicalRecordDTos> _getMedicalRecord;
        private readonly IValidator<CreateUpdateMedicalRecordDTos> _validator;

        public MedicalRecordController(ICreateMedicalRecord<CreateUpdateMedicalRecordDTos, MedicalRecordDTos> createMedicalRecord,
            IGetByIdMedicalRecord<MedicalRecordDTos> getByIdMedicalRecord, IUpdateMedicalRecord<CreateUpdateMedicalRecordDTos, MedicalRecordDTos> updateMedicalRecord,
            IGetMedicalRecord<MedicalRecordDTos> getMedicalRecord, IValidator<CreateUpdateMedicalRecordDTos> validator)
        {
            _createMedicalRecord = createMedicalRecord;
            _getByIdMedicalRecord = getByIdMedicalRecord;
            _updateMedicalRecord = updateMedicalRecord; 
            _getMedicalRecord = getMedicalRecord;
            _validator = validator;
        }


        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MedicalRecordDTos>> GetMedicalRecordAsync(CancellationToken cancellationToken)
        {
            var medicalRecords = await _getMedicalRecord.GetAllAsync(cancellationToken);

            return Ok(medicalRecords);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute]int id,CancellationToken cancellationToken)
        {
            var result = await _getByIdMedicalRecord.GetByIdAsync(id,cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Error);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateMedicalRecordAsync([FromBody] CreateUpdateMedicalRecordDTos medicalRecordDTos, CancellationToken cancellationToken)
        {

            var resultValidation = await _validator.ValidateAsync(medicalRecordDTos,cancellationToken);
            if (!resultValidation.IsValid)
            {
                return BadRequest(resultValidation.Errors);
            }

            var result = await _createMedicalRecord.AddAsync(medicalRecordDTos, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMedicalRecordAsync([FromRoute] int id, [FromBody] CreateUpdateMedicalRecordDTos updateMedicalRecordDTos, CancellationToken cancellationToken)
        {

            var resultValidation = await _validator.ValidateAsync(updateMedicalRecordDTos,cancellationToken);
            if (!resultValidation.IsValid)
            {
                return BadRequest(resultValidation.Errors);
            }

            var result = await _getByIdMedicalRecord.GetByIdAsync(id,cancellationToken);
            if (result.IsSuccess)
            {
                var medicalRecordNew = await _updateMedicalRecord.UpdateAsync(id, updateMedicalRecordDTos,cancellationToken);
                return Ok(medicalRecordNew.Value);
            }

            return NotFound(result.Error);
        }
    }
}
