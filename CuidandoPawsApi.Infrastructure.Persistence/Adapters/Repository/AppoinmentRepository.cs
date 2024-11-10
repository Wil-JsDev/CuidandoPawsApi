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
    
    public async Task<IEnumerable<ServiceCatalog>> CheckAvailabilityAsync(int serviceId, DateTime date)
    {
        
        var query = await _context.Set<ServiceCatalog>().AsQueryable()
            .Where(s => s.Id == serviceId && s.CreatedAt >= date)
            .ToListAsync();
        
        return query;
    }

    public async Task<IEnumerable<ServiceCatalog>> GetAvailabilityServiceAsync(int serviceCatalog, bool isActive)
    {
        var query = await _context.Set<ServiceCatalog>().AsQueryable()
            .Where(x => x.Id == serviceCatalog && x.IsAvaible == isActive)
            .ToListAsync();
        
        return query;
    }

    public async Task<Appoinment> GetLastAddedAppoinmentAsync(int appoinmentId)
    {
        var query = await _context.Set<Appoinment>().AsQueryable()   
                                    .OrderByDescending(s => s.Id == appoinmentId)
                                    .FirstOrDefaultAsync();
        
        return query;
    }
}