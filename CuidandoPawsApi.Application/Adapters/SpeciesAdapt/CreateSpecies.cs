using AutoMapper;
using CuidandoPawsApi.Application.DTOs.Species;
using CuidandoPawsApi.Domain.Models;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Domain.Ports.UseCase.Species;
using CuidandoPawsApi.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.Adapters.SpeciesAdapter
{
    public class CreateSpecies : ICreateSpecies<CreateUpdateSpecieDTos, SpeciesDTos>
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IMapper _mapper;

        public CreateSpecies(ISpeciesRepository speciesRepository, IMapper mapper)
        {
            _speciesRepository = speciesRepository;
            _mapper = mapper;
        }

        public async Task <ResultT<SpeciesDTos>> AddAsync(CreateUpdateSpecieDTos dtoStatus, CancellationToken cancellationToken)
        {
            var species = _mapper.Map<Species>(dtoStatus);

            if (species != null)
            {
                await  _speciesRepository.AddAsync(species, cancellationToken);
                var speciesDto = _mapper.Map<SpeciesDTos>(species);
                return ResultT<SpeciesDTos>.Success(speciesDto);
            }

            return ResultT<SpeciesDTos>.Failure(Error.Failure("400", "Error entering data"));
        }
    }
}
