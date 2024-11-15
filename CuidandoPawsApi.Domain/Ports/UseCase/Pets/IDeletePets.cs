namespace CuidandoPawsApi.Domain.Ports.UseCase;

public interface IDeletePets<TDto>
{
    Task<TDto> DeleteAsync(int petId, CancellationToken cancellationToken);
}