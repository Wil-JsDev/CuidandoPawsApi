using AutoMapper;
using CuidandoPawsApi.Application.DTOs.Species;
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
    public class GetByIdSpecies : IGetByIdSpecies<SpeciesDTos>
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IMapper _mapper;

        public GetByIdSpecies(ISpeciesRepository speciesRepository, IMapper mapper)
        {
            _speciesRepository = speciesRepository;
            _mapper = mapper;
        }

        public async Task<ResultT<SpeciesDTos>> GetById(int id, CancellationToken cancellationToken)
        {
            var speciesId = await _speciesRepository.GetByIdAsync(id, cancellationToken);

            if (speciesId != null)
            {
                var speciesDto = _mapper.Map<SpeciesDTos>(speciesId);
                return ResultT<SpeciesDTos>.Success(speciesDto);
            }

            return ResultT<SpeciesDTos>.Failure(Error.NotFound("404", "id not found"));
        }
    }
}
