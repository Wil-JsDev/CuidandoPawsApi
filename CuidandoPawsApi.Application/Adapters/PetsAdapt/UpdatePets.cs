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
    public class UpdatePets : IUpdatePets<UpdatePetsDTos, PetsDTos>
    {
        private readonly IPetsRepository _petsRepository;
        private readonly IMapper _mapper;

        public UpdatePets(IPetsRepository petsRepository, IMapper mapper)
        {
            _petsRepository = petsRepository;
            _mapper = mapper;
        }

        public async Task<PetsDTos> UpdateAsync(int id, UpdatePetsDTos dto, CancellationToken cancellationToken)
        {
            var pet = await _petsRepository.GetByIdAsync(id,cancellationToken);
            if (pet != null)
            {
                pet = _mapper.Map<UpdatePetsDTos, Pets>(dto,pet);
                await _petsRepository.UpdateAsync(pet);

                var petDto = _mapper.Map<PetsDTos>(pet);
                return petDto;
            }

            return null;
        }
    }
}
