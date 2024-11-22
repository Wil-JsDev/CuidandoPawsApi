using AutoMapper;
using CuidandoPawsApi.Application.DTOs.Pets;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Domain.Ports.UseCase.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.Adapters.PetsAdapt
{
    public class GetPetsLastAddedOfDay : IGetPetsLastAddedOfDay<PetsDTos>
    {
        private readonly IPetsRepository _petsRepository;
        private readonly IMapper _mapper; 

        public GetPetsLastAddedOfDay(IPetsRepository petsRepository, IMapper mapper)
        {
            _petsRepository = petsRepository;
            _mapper = mapper;
        }

        public async Task<PetsDTos> GetLastAddedPetsOfDayAsync(CancellationToken cancellationToken)
        {
            DateTime today = DateTime.UtcNow;
            var pets = await _petsRepository.GetLastAddedPetsOfDayAsync(today,cancellationToken);   
            return pets != null ? _mapper.Map<PetsDTos>(pets) : new PetsDTos();
        }
    }
}
