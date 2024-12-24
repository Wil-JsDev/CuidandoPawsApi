using AutoMapper;
using CuidandoPawsApi.Application.DTOs.Species;
using CuidandoPawsApi.Domain.Enum;
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
            var sortMethods = CreateSortMethods();

            if (sortMethods.TryGetValue((sort.ToLower(), direction.ToLower()), out var sortMethod))
            {
                var species = await sortMethod(cancellationToken);  
                var speciesDto = _mapper.Map<IEnumerable<SpeciesDTos>>(species);  
                return ResultT<IEnumerable<SpeciesDTos>>.Success(speciesDto);
            }
            return ResultT<IEnumerable<SpeciesDTos>>.Failure(Error.Failure("400","Invalid direction parameter. Accepted values are 'asc' or 'desc'."));
        }

        private Dictionary<(string sort, string direction), Func<CancellationToken, Task<IEnumerable<Species>>>> CreateSortMethods()
        {
            return new Dictionary<(string sort, string direction), Func<CancellationToken, Task<IEnumerable<Species>>>>()
        {
        { ("id", "asc"), async cancellationToken => await _speciesRepository.GetOrdereByIdAscSpeciesAsync(cancellationToken) },
        { ("id", "desc"), async cancellationToken => await _speciesRepository.GetOrdereByIdDescSpeciesAsync(cancellationToken) },
        { ("name", "asc"), async cancellationToken => await _speciesRepository.GetOrdereByNameAscSpeciesAsync(cancellationToken) },
        { ("name", "desc"), async cancellationToken => await _speciesRepository.GetOrdereByNameDescSpeciesAsync(cancellationToken) }
        };
        }
    }
}
