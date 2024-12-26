using CuidandoPawsApi.Domain.Utils;

namespace CuidandoPawsApi.Domain.Ports.UseCase;

public interface IDeletePets<TDto>
{
    Task <ResultT<TDto>> DeleteAsync(int petId, CancellationToken cancellationToken);
}