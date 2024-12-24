using CuidandoPawsApi.Domain.Models;
using CuidandoPawsApi.Domain.Pagination;
using CuidandoPawsApi.Domain.Utils;

namespace CuidandoPawsApi.Domain.Ports.UseCase;

public interface IGetPagedPets<TDto>
{
    Task <ResultT<PagedResult<TDto>>> ListWithPaginationAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}