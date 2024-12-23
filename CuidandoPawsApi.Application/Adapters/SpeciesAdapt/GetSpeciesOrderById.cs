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
    public class GetSpeciesOrderById : IGetSpeciesOrderById<SpeciesDTos>
    {

        private readonly ISpeciesRepository _speciesRepository;
        private readonly IMapper _mapper;

        public GetSpeciesOrderById(ISpeciesRepository speciesRepository, IMapper mapper)
        {
            _speciesRepository = speciesRepository;
            _mapper = mapper;
        }

        public async Task <ResultT<IEnumerable<SpeciesDTos>>> GetOrderedByIdAsync(string sort,string direction,CancellationToken cancellationToken)
        {

            if (sort == "id".ToLower() && direction == "asc".ToLower())
            {
                var speciesOrderById = await _speciesRepository.GetOrdereByIdAscSpeciesAsync(cancellationToken);
                var speciesDto = _mapper.Map<IEnumerable<SpeciesDTos>>(speciesOrderById);
                return ResultT<IEnumerable<SpeciesDTos>>.Success(speciesDto);
            }
            else if (sort == "id".ToLower() && direction == "desc".ToLower())
            {
                var speciesOrderByIdDesc = await _speciesRepository.GetOrdereByIdDescSpeciesAsync(cancellationToken);
                var speciesDto = _mapper.Map<IEnumerable<SpeciesDTos>>(speciesOrderByIdDesc);
                return ResultT<IEnumerable<SpeciesDTos>>.Success(speciesDto);
            }
            else if (sort == "name".ToLower() && direction == "asc".ToLower())
            {
                var speciesOrderByNameAsc = await _speciesRepository.GetOrdereByNameAscSpeciesAsync(cancellationToken);
                var speciesDto = _mapper.Map<IEnumerable<SpeciesDTos>>(speciesOrderByNameAsc);
                return ResultT<IEnumerable<SpeciesDTos>>.Success(speciesDto);
            }
            else if (sort == "name".ToLower() && direction == "desc".ToLower())
            {
                var speciesOrderByNameDesc = await _speciesRepository.GetOrdereByNameDescSpeciesAsync(cancellationToken);
                var speciesDto = _mapper.Map<IEnumerable<SpeciesDTos>>(speciesOrderByNameDesc);
                return ResultT<IEnumerable<SpeciesDTos>>.Success(speciesDto);
            }
            return ResultT<IEnumerable<SpeciesDTos>>.Failure(Error.Failure("400","Invalid direction parameter. Accepted values are 'asc' or 'desc'."));
        }
    }
}
