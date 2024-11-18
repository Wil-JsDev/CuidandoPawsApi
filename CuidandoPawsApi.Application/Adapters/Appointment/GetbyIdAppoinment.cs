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
    public class GetbyIdAppoinment : IGetByIdAppoinment<AppoinmentDTos>
    {
        private readonly IAppoinmentRepository _repository;
        private readonly IMapper _mapper;

        public GetbyIdAppoinment(IAppoinmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AppoinmentDTos> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var idEntitie = await _repository.GetByIdAsync(id, cancellationToken);

            if (idEntitie != null)
            {
                var appoinmenIdDto = _mapper.Map<AppoinmentDTos>(idEntitie);
                return appoinmenIdDto;
            }

            return null;
        }
    }
}
