using CuidandoPawsApi.Domain.Models;
using CuidandoPawsApi.Domain.Utils;

namespace CuidandoPawsApi.Domain.Ports.UseCase;

public interface ICreatePets<TDtoStatus,TDto>
{
    Task <ResultT<TDto>> AddAsync(TDtoStatus petDto, CancellationToken cancellationToken);
}