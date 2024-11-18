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
        private readonly CancellationToken _cancellationToken;
        private readonly IMapper _mapper;

        public UpdateAppoinment(IAppoinmentRepository appoinmentRepository, CancellationToken cancellationToken, 
            IMapper mapper)
        {
            _appoinmentRepository = appoinmentRepository;
            this._cancellationToken = cancellationToken;
            _mapper = mapper;
        }

        public async Task<AppoinmentDTos> UpdateAsync(int id, CreateUpdateAppoinmentDTos dtoStatus)
        {
            var appoinmentId = await _appoinmentRepository.GetByIdAsync(id,_cancellationToken);

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
