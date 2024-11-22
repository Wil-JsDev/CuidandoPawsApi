using CuidandoPawsApi.Domain.Models;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Domain.Pagination;
using CuidandoPawsApi.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CuidandoPawsApi.Infrastructure.Persistence.Adapters.Repository;

public class PetsRepository : GenericRepository<Pets>, IPetsRepository
{
    public PetsRepository(CuidandoPawsContext context) : base(context)
    {
        
    }

    public async Task<PagedResult<Pets>> GetPagedPetsAsync(int pageNumber, int pageSize,CancellationToken cancellationToken)
    {
        
        var totalRecords = await _context.Set<Pets>().AsNoTracking().CountAsync();

        var pets = await _context.Set<Pets>().AsNoTracking()
            .OrderBy(p => p.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var pagedResponse = new PagedResult<Pets>(pets, pageNumber, pageSize,totalRecords);
        
        return pagedResponse;
    }

    public async Task<Pets> GetLastAddedPetsOfDayAsync(DateTime day,CancellationToken cancellationToken)
    {
        var query = await _context.Set<Pets>().AsQueryable()
            .Where(x => x.DateOfEntry < day)
            .OrderByDescending(p => p.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        return query;
    }
}