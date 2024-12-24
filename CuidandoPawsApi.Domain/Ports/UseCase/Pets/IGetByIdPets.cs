using CuidandoPawsApi.Domain.Utils;

namespace CuidandoPawsApi.Domain.Ports.UseCase;

public interface IGetByIdPets<TDto>
{
    Task <ResultT<TDto>> GetByIdAsync(int pet, CancellationToken cancellationToken);
}