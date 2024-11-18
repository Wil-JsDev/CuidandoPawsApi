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
    public class DeleteAppoinment : IDeleteAppoinment<AppoinmentDTos>
    {
        private readonly IAppoinmentRepository _repository;
        private readonly IMapper _mapper;

        public DeleteAppoinment(IAppoinmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AppoinmentDTos> DeleteAppoinmentAsync(int id, CancellationToken cancellationToken)
        {

            var appoinmentId = await _repository.GetByIdAsync(id, cancellationToken);
            if (appoinmentId != null)
            {
                await _repository.DeleteAsync(appoinmentId, cancellationToken);
                var appoinmentDto = _mapper.Map<AppoinmentDTos>(appoinmentId);
                return appoinmentDto;
            }

            return null;
        }
    }
}
