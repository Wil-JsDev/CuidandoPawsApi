﻿using AutoMapper;
using CuidandoPawsApi.Application.DTOs.MedicalRecord;
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
    public class DeleteMedicalRecord : IDeleteMedicalRecord<MedicalRecordDTos>
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IMapper _mapper;

        public DeleteMedicalRecord(IMedicalRecordRepository medicalRecordRepository, IMapper mapper)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _mapper = mapper;
        }


        public async Task <ResultT<MedicalRecordDTos>> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var medicalRecordId = await _medicalRecordRepository.GetByIdAsync(id,cancellationToken);
            if (medicalRecordId != null)
            {
               await _medicalRecordRepository.DeleteAsync(medicalRecordId,cancellationToken);

                var medicalRecordDto = _mapper.Map<MedicalRecordDTos>(medicalRecordId);
                return ResultT<MedicalRecordDTos>.Success(medicalRecordDto);
            }

            return ResultT<MedicalRecordDTos>.Failure(Error.NotFound("404", "Id not found"));
        }
    }
}
