using AutoMapper;
using CuidandoPawsApi.Application.DTOs.Appoinment;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Domain.Ports.UseCase.Appoinment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.Adapters.Appointment
{
    public class GetAllAppoinment : IGetAppoinment<AppoinmentDTos>
    {
        private readonly IAppoinmentRepository _appoinmentRepository;
        private readonly IMapper _mapper;
        public GetAllAppoinment(IAppoinmentRepository appoinmentRepository, IMapper mapper)
        {
            _appoinmentRepository = appoinmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppoinmentDTos>> GetAllAsync(CancellationToken cancellationToken)
        {
            var appoinmentAll = await _appoinmentRepository.GetAllAsync(cancellationToken);
            return appoinmentAll.Select(x => _mapper.Map<AppoinmentDTos>(x));
        }
    }
}
