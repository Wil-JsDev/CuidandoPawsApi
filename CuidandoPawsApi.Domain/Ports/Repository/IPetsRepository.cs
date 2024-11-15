using CuidandoPawsApi.Domain.Models;
using CuidandoPawsApi.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.Repository
{
    public interface IPetsRepository : IGenericRepository<Pets>
    {

        Task<PagedResult<Pets>> GetPagedPetsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<Pets> GetLastAddedPetsOfDayAsync(DateTime day,CancellationToken cancellationToken);
    }
}
