using AutoMapper;
using CuidandoPawsApi.Application.DTOs.Pets;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Domain.Ports.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.Adapters.PetsAdapt
{
    public class DeletePets : IDeletePets<PetsDTos>
    {
        private readonly IPetsRepository _petsRepository; 
        private readonly IMapper _mapper;

        public DeletePets(IPetsRepository petsRepository, IMapper mapper)
        {
            _petsRepository = petsRepository;
            _mapper = mapper;
        }

        public async Task<PetsDTos> DeleteAsync(int petId, CancellationToken cancellationToken)
        {
            var pet = await _petsRepository.GetByIdAsync(petId, cancellationToken);

            if (pet != null)
            {
                await _petsRepository.DeleteAsync(pet,cancellationToken);
                var petDto = _mapper.Map<PetsDTos>(pet);
                return petDto;
            }

            return null;
        }
    }
}
