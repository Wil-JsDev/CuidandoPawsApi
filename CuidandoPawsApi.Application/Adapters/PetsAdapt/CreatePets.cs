using AutoMapper;
using CuidandoPawsApi.Application.DTOs.Pets;
using CuidandoPawsApi.Domain.Models;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Domain.Ports.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.Adapters.PetsAdapt
{
    public class CreatePets : ICreatePets<CreatePetsDTos, PetsDTos>
    {
        private readonly IPetsRepository _petsRepository;
        private readonly IMapper _mapper;

        public CreatePets(IPetsRepository petsRepository, IMapper mapper)
        {
            _petsRepository = petsRepository;
            _mapper = mapper;
        }

        public async Task<PetsDTos> AddAsync(CreatePetsDTos petDto, CancellationToken cancellationToken)
        {
            var pets = _mapper.Map<Pets>(petDto);
            if (pets != null)
            {
                await _petsRepository.AddAsync(pets,cancellationToken);
                var petsDto = _mapper.Map<PetsDTos>(pets);
                return petsDto;
            }

            return null;
        }
    }
}
