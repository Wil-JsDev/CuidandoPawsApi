namespace CuidandoPawsApi.Domain.Ports.UseCase;

public interface IUpdatePets<TDtoStatus, TDto>
{
    Task<TDto> UpdateAsync(int id,TDtoStatus dto);
}