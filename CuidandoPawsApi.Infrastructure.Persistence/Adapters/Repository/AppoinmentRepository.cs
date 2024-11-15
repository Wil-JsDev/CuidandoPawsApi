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
    
    public async Task<IEnumerable<ServiceCatalog>> CheckAvailabilityAsync(int serviceId, CancellationToken cancellationToken)
    {
        
        var query = await _context.Set<ServiceCatalog>().AsQueryable()
            .Where(s => s.Id == serviceId)
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

    public async Task<Appoinment> GetLastAppoinmentAddedOnDateAsync(DateTime dateTime, CancellationToken cancellationToken)
    {
        var query = await _context.Set<Appoinment>().AsQueryable()  
                                    .Where(x => x.ReservationDate < dateTime)
                                    .OrderByDescending(s => s.Id)
                                    .FirstOrDefaultAsync(cancellationToken);
        
        return query;
    }
}