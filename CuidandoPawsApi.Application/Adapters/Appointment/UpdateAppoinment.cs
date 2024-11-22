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
    public class UpdateAppoinment : IUpdateAppoinment<CreateUpdateAppoinmentDTos, AppoinmentDTos>
    {
        private readonly IAppoinmentRepository _appoinmentRepository;
        private readonly IMapper _mapper;

        public UpdateAppoinment(IAppoinmentRepository appoinmentRepository, IMapper mapper)
        {
            _appoinmentRepository = appoinmentRepository;
            _mapper = mapper;
        }

        public async Task<AppoinmentDTos> UpdateAsync(int id, CreateUpdateAppoinmentDTos dtoStatus, CancellationToken cancellationToken)
        {
            var appoinmentId = await _appoinmentRepository.GetByIdAsync(id, cancellationToken);

            if (appoinmentId != null)
            {
                appoinmentId = _mapper.Map<CreateUpdateAppoinmentDTos, Appoinment>(dtoStatus, appoinmentId);
                await _appoinmentRepository.UpdateAsync(appoinmentId);
                var appoinmentDto = _mapper.Map<AppoinmentDTos>(appoinmentId);
                return appoinmentDto;
            }


            return null;
        }
    }
}
