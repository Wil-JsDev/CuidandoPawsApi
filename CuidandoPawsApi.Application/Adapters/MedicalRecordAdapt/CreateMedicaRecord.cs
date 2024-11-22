using AutoMapper;
using CuidandoPawsApi.Application.DTOs.MedicalRecord;
using CuidandoPawsApi.Domain.Models;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Domain.Ports.UseCase.MedicalRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.Adapters.MedicalRecordAdapt
{
    public class CreateMedicaRecord : ICreateMedicalRecord<CreateUpdateMedicalRecordDTos, MedicalRecordDTos>
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IMapper _mapper;

        public CreateMedicaRecord(IMedicalRecordRepository medicalRecordRepository, IMapper mapper)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _mapper = mapper;
        }

        public async Task<MedicalRecordDTos> AddAsync(CreateUpdateMedicalRecordDTos dtoStatus, CancellationToken cancellationToken)
        {
            var medicalRecord = _mapper.Map<MedicalRecord>(dtoStatus);

            if (medicalRecord != null)
            {
                await _medicalRecordRepository.AddAsync(medicalRecord,cancellationToken);
                var medicalRecordDto = _mapper.Map<MedicalRecordDTos>(medicalRecord);
                return medicalRecordDto;
            }

            return null;
        }
    }
}
