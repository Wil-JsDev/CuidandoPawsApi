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
    public class GetSpeciesOrderById : IGetSpeciesOrderById<SpeciesDTos>
    {

        private readonly ISpeciesRepository _speciesRepository;
        private readonly IMapper _mapper;

        public GetSpeciesOrderById(ISpeciesRepository speciesRepository, IMapper mapper)
        {
            _speciesRepository = speciesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SpeciesDTos>> GetOrderedByIdAsync(string direction,CancellationToken cancellationToken)
        {

            if (direction == "asc".ToLower())
            {
                var speciesOrderById = await _speciesRepository.GetOrderedByIdAsync(cancellationToken);
                var speciesDto = _mapper.Map<IEnumerable<SpeciesDTos>>(speciesOrderById);
                return speciesDto;
            }
            else if (direction == "desc".ToLower())
            {
                var speciesOrderByIdDesc = await _speciesRepository.GetOrdereByIdDescSpeciesAsync(cancellationToken);
                var speciesDto = _mapper.Map<IEnumerable<SpeciesDTos>>(speciesOrderByIdDesc);
                return speciesDto;
            }

            return null;
        }
    }
}
