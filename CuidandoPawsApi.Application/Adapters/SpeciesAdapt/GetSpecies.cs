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
    public class GetSpecies : IGetSpecies<SpeciesDTos>
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IMapper _mapper;

        public GetSpecies(ISpeciesRepository speciesRepository, IMapper mapper)
        {
            _speciesRepository = speciesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SpeciesDTos>> GetAllAsync(CancellationToken cancellationToken)
        {
            var speciesAll = await _speciesRepository.GetAllAsync(cancellationToken);
            return speciesAll.Select(x => _mapper.Map<SpeciesDTos>(x));
        }
    }
}
