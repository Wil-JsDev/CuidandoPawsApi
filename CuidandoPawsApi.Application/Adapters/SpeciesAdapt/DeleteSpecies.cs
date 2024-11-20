using AutoMapper;
using CuidandoPawsApi.Application.DTOs.Species;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Domain.Ports.UseCase.Species;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.Adapters.SpeciesAdapt
{
    public class DeleteSpecies : IDeleteSpecies<SpeciesDTos>
    {

        private readonly ISpeciesRepository _speciesRepository;
        private readonly IMapper _mapper;

        public DeleteSpecies(ISpeciesRepository speciesRepository, IMapper mapper)
        {
            _speciesRepository = speciesRepository;
            _mapper = mapper;
        }

        public async Task<SpeciesDTos> DeleteSpeciesAsync(int id, CancellationToken cancellationToken)
        {

            var species = await _speciesRepository.GetByIdAsync(id,cancellationToken);

            if (species != null)
            {
                await _speciesRepository.DeleteAsync(species,cancellationToken);
                var speciesDto = _mapper.Map<SpeciesDTos>(species);
                return speciesDto;
            }

            return null;
        }
    }
}
