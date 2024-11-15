namespace CuidandoPawsApi.Domain.Ports.UseCase;

public interface IGetByIdPets<TDto>
{
    Task<TDto> GetByIdAsync(int pet, CancellationToken cancellationToken);
}