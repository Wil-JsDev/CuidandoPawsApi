using AutoMapper;
using CuidandoPawsApi.Application.DTOs.Appoinment;
using CuidandoPawsApi.Application.DTOs.MedicalRecord;
using CuidandoPawsApi.Domain.Models;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Domain.Ports.UseCase.MedicalRecord;
using CuidandoPawsApi.Domain.Utils;
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

        public UpdateMedicalRecord(IMedicalRecordRepository medicalRecordRepository, IMapper mapper)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _mapper = mapper;
        }

        public async Task <ResultT<MedicalRecordDTos>> UpdateAsync(int id, CreateUpdateMedicalRecordDTos dtoStatus, CancellationToken cancellationToken)
        {
            var medicalRecord = await _medicalRecordRepository.GetByIdAsync(id, cancellationToken);
            if (medicalRecord != null)
            {
                medicalRecord = _mapper.Map<CreateUpdateMedicalRecordDTos, MedicalRecord>(dtoStatus, medicalRecord);
                await _medicalRecordRepository.UpdateAsync(medicalRecord);
                var medicalRecordDTo = _mapper.Map<MedicalRecordDTos>(medicalRecord);
                return ResultT<MedicalRecordDTos>.Success(medicalRecordDTo);
            }

            return ResultT<MedicalRecordDTos>.Failure(Error.NotFound("404", "id not found"));
        }
    }
}
