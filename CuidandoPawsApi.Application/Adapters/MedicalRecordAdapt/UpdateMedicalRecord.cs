using AutoMapper;
using CuidandoPawsApi.Application.DTOs.Appoinment;
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
    public class UpdateMedicalRecord : IUpdateMedicalRecord<CreateUpdateMedicalRecordDTos, MedicalRecordDTos>
    {

        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IMapper _mapper;
        private readonly CancellationToken _cancellationToken;
        public UpdateMedicalRecord(IMedicalRecordRepository medicalRecordRepository, IMapper mapper, CancellationToken cancellationToken)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _mapper = mapper;
            _cancellationToken = cancellationToken;
        }

        public async Task<MedicalRecordDTos> UpdateAsync(int id, CreateUpdateMedicalRecordDTos dtoStatus)
        {
            var medicalRecord = await _medicalRecordRepository.GetByIdAsync(id, _cancellationToken);
            if (medicalRecord != null)
            {
                medicalRecord = _mapper.Map<CreateUpdateMedicalRecordDTos, MedicalRecord>(dtoStatus, medicalRecord);
                await _medicalRecordRepository.UpdateAsync(medicalRecord);
                var medicalRecordDTo = _mapper.Map<MedicalRecordDTos>(medicalRecord);
                return medicalRecordDTo;
            }

            return null;
        }
    }
}
