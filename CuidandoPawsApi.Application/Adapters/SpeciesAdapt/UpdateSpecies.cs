using AutoMapper;
using CuidandoPawsApi.Application.DTOs.Species;
using CuidandoPawsApi.Domain.Models;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Domain.Ports.UseCase.Species;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.Adapters.SpeciesAdapt
{
    public class UpdateSpecies : IUpdateSpecies<CreateUpdateSpecieDTos, SpeciesDTos>
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IMapper _mapper;
        private readonly CancellationToken _cancellationToken;

        public UpdateSpecies(ISpeciesRepository speciesRepository, IMapper mapper, CancellationToken cancellationToken)
        {
            _speciesRepository = speciesRepository;
            _mapper = mapper;
            _cancellationToken = cancellationToken;
        }

        public async Task<SpeciesDTos> UpdateAsync(int id, CreateUpdateSpecieDTos dtoStatus)
        {
            var speciesId = await _speciesRepository.GetByIdAsync(id,_cancellationToken);

            if (speciesId != null)
            {
                speciesId = _mapper.Map<CreateUpdateSpecieDTos, Species>(dtoStatus, speciesId);
                await _speciesRepository.UpdateAsync(speciesId);
                var speciesDto = _mapper.Map<SpeciesDTos>(speciesId);
                return speciesDto;
            }

            return null;
        }
    }
}
