using CuidandoPawsApi.Domain.Utils;

namespace CuidandoPawsApi.Domain.Ports.UseCase;

public interface IUpdatePets<TDtoStatus, TDto>
{
    Task <ResultT<TDto>> UpdateAsync(int id,TDtoStatus dto, CancellationToken cancellationToken);
}