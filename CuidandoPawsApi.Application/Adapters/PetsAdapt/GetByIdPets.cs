using AutoMapper;
using CuidandoPawsApi.Application.DTOs.Pets;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Domain.Ports.UseCase;
using CuidandoPawsApi.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.Adapters.PetsAdapt
{
    public class GetByIdPets : IGetByIdPets<PetsDTos>
    {
        private readonly IPetsRepository _petsRepository;
        private readonly IMapper _mapper;

        public GetByIdPets(IPetsRepository petsRepository, IMapper mapper)
        {
            _petsRepository = petsRepository;
            _mapper = mapper;
        }

        public async Task <ResultT<PetsDTos>> GetByIdAsync(int pet, CancellationToken cancellationToken)
        {
            var petId = await _petsRepository.GetByIdAsync(pet, cancellationToken);
            if (petId != null)
            {
                var petDto = _mapper.Map<PetsDTos>(petId);
                return ResultT<PetsDTos>.Success(petDto);
            }

            return ResultT<PetsDTos>.Failure(Error.NotFound("404", "Id not found"));
        }
    }
}
