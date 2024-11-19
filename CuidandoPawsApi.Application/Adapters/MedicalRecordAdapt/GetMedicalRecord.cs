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
    public class GetMedicalRecord : IGetMedicalRecord<MedicalRecordDTos>
    {

        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IMapper _mapper;

        public GetMedicalRecord(IMedicalRecordRepository medicalRecordRepository, IMapper mapper)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MedicalRecordDTos>> GetAllAsync(CancellationToken cancellationToken)
        {
            var medicalRecord = await _medicalRecordRepository.GetAllAsync(cancellationToken);

            return medicalRecord.Select(x => _mapper.Map<MedicalRecordDTos>(x));
        }
    }
}
