using AutoMapper;
using CuidandoPawsApi.Application.DTOs.MedicalRecord;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Domain.Ports.UseCase.MedicalRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.Adapters.MedicalRecordAdapt
{
    public class GetByIdMedicalRecord : IGetByIdMedicalRecord<MedicalRecordDTos>
    {

        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IMapper _mapper;

        public GetByIdMedicalRecord(IMedicalRecordRepository medicalRecordRepository, IMapper mapper)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _mapper = mapper;
        }

        public async Task<MedicalRecordDTos> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var medicalRecordId = await _medicalRecordRepository.GetByIdAsync(id, cancellationToken);
            if (medicalRecordId != null)
            {
                var medicalRecordDto = _mapper.Map<MedicalRecordDTos>(medicalRecordId);
                return medicalRecordDto;
            }

            return null;
        }
    }
}
