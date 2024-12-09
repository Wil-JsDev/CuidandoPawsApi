using Asp.Versioning;
using CuidandoPawsApi.Application.Adapters.PetsAdapt;
using CuidandoPawsApi.Application.DTOs.MedicalRecord;
using CuidandoPawsApi.Application.DTOs.Pets;
using CuidandoPawsApi.Domain.Ports.UseCase;
using CuidandoPawsApi.Domain.Ports.UseCase.MedicalRecord;
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

        public MedicalRecordController(ICreateMedicalRecord<CreateUpdateMedicalRecordDTos, MedicalRecordDTos> createMedicalRecord,
            IGetByIdMedicalRecord<MedicalRecordDTos> getByIdMedicalRecord, IUpdateMedicalRecord<CreateUpdateMedicalRecordDTos, MedicalRecordDTos> updateMedicalRecord,
            IGetMedicalRecord<MedicalRecordDTos> getMedicalRecord)
        {
            _createMedicalRecord = createMedicalRecord;
            _getByIdMedicalRecord = getByIdMedicalRecord;
            _updateMedicalRecord = updateMedicalRecord; 
            _getMedicalRecord = getMedicalRecord;
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
        public async Task<ActionResult<MedicalRecordDTos>> GetByIdAsync([FromRoute]int id,CancellationToken cancellationToken)
        {
            var medicalRecord = await _getByIdMedicalRecord.GetByIdAsync(id,cancellationToken);

            return medicalRecord == null ? NotFound() : Ok(medicalRecord);
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MedicalRecordDTos>> CreateMedicalRecordAsync([FromBody] CreateUpdateMedicalRecordDTos medicalRecordDTos, CancellationToken cancellationToken)
        {
            var medicalRecord = await _createMedicalRecord.AddAsync(medicalRecordDTos, cancellationToken);

            return CreatedAtAction(nameof(GetByIdAsync), new {Id = medicalRecord.MedicalRecordId}, medicalRecord);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MedicalRecordDTos>> UpdateMedicalRecordAsync([FromRoute] int id, [FromBody] CreateUpdateMedicalRecordDTos updateMedicalRecordDTos, CancellationToken cancellationToken)
        {
            var medicalRecordId = await _getByIdMedicalRecord.GetByIdAsync(id,cancellationToken);
            if (medicalRecordId != null)
            {
                var medicalRecordNew = await _updateMedicalRecord.UpdateAsync(id, updateMedicalRecordDTos,cancellationToken);
                return Ok(medicalRecordNew);
            }

            return NotFound(medicalRecordId);
        }
    }
}
