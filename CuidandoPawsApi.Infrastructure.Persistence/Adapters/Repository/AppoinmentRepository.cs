using CuidandoPawsApi.Domain.Models;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CuidandoPawsApi.Infrastructure.Persistence.Adapters.Repository;

public class AppoinmentRepository : GenericRepository<Appoinment>, IAppoinmentRepository
{
    public AppoinmentRepository(CuidandoPawsContext context) : base(context)    
    {
        
    }
    
    public async Task<IEnumerable<ServiceCatalog>> CheckAvailabilityAsync(int serviceId, DateTime date, CancellationToken cancellationToken)
    {
        
        var query = await _context.Set<ServiceCatalog>().AsQueryable()
            .Where(s => s.Id == serviceId && s.CreatedAt >= date)
            .ToListAsync(cancellationToken);
        
        return query;
    }

    public async Task<IEnumerable<ServiceCatalog>> GetAvailabilityServiceAsync(int serviceCatalog, bool isActive, CancellationToken cancellationToken )
    {
        var query = await _context.Set<ServiceCatalog>().AsQueryable()
            .Where(x => x.Id == serviceCatalog && x.IsAvaible == isActive)
            .ToListAsync(cancellationToken);
        
        return query;
    }

    public async Task<Appoinment> GetLastAddedAppoinmentAsync(int appoinmentId, CancellationToken cancellationToken)
    {
        var query = await _context.Set<Appoinment>().AsQueryable()   
                                    .OrderByDescending(s => s.Id == appoinmentId)
                                    .FirstOrDefaultAsync(cancellationToken);
        
        return query;
    }
}