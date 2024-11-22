using AutoMapper;
using CuidandoPawsApi.Application.DTOs.Appoinment;
using CuidandoPawsApi.Domain.Models;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Domain.Ports.UseCase.Appoinment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.Adapters.Appointment
{
    public class CreateAppoinment : ICreateAppoinment<CreateUpdateAppoinmentDTos, AppoinmentDTos>
    {
        private readonly IAppoinmentRepository _appoinmentRepository;
        private IMapper _mapper;
        public CreateAppoinment(IAppoinmentRepository appoinmentRepository, IMapper mapper)
        {
            _appoinmentRepository = appoinmentRepository;
            _mapper = mapper;
        }

        public async Task<AppoinmentDTos> AddAsync(CreateUpdateAppoinmentDTos appoinmentDTos, CancellationToken cancellationToken)
        {
            var appoinment = _mapper.Map<Appoinment>(appoinmentDTos);

            if (appoinmentDTos != null)
            {
                await _appoinmentRepository.AddAsync(appoinment, cancellationToken);
                var appoinmentDto = _mapper.Map<AppoinmentDTos>(appoinment);
                return appoinmentDto;
            }



            return null;
        }
    }
}
