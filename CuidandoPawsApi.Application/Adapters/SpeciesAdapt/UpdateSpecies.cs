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

namespace CuidandoPawsApi.Application.Adapters.SpeciesAdapt
{
    public class UpdateSpecies : IUpdateSpecies<CreateUpdateSpecieDTos, SpeciesDTos>
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IMapper _mapper;

        public UpdateSpecies(ISpeciesRepository speciesRepository, IMapper mapper)
        {
            _speciesRepository = speciesRepository;
            _mapper = mapper;
        }

        public async Task<ResultT<SpeciesDTos>> UpdateAsync(int id, CreateUpdateSpecieDTos dtoStatus, CancellationToken cancellationToken)
        {
            var speciesId = await _speciesRepository.GetByIdAsync(id, cancellationToken);

            if (speciesId != null)
            {
                speciesId = _mapper.Map<CreateUpdateSpecieDTos, Species>(dtoStatus, speciesId);
                await _speciesRepository.UpdateAsync(speciesId);
                var speciesDto = _mapper.Map<SpeciesDTos>(speciesId);
                return ResultT<SpeciesDTos>.Success(speciesDto);
            }

            return ResultT<SpeciesDTos>.Failure(Error.NotFound("404", "id not found"));
        }
    }
}
