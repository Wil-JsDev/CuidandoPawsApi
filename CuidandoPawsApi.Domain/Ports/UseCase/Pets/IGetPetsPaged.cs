using CuidandoPawsApi.Domain.Models;
using CuidandoPawsApi.Domain.Pagination;

namespace CuidandoPawsApi.Domain.Ports.UseCase;

public interface IGetPagedPets<TDto>
{
    Task<PagedResult<TDto>> ListWithPaginationAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}