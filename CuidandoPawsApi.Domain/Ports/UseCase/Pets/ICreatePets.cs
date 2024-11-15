using CuidandoPawsApi.Domain.Models;

namespace CuidandoPawsApi.Domain.Ports.UseCase;

public interface ICreatePets<TDtoStatus,TDto>
{
    Task<TDto> AddAsync(TDtoStatus petDto, CancellationToken cancellationToken);
}