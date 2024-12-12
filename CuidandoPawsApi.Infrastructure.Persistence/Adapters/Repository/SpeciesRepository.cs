using CuidandoPawsApi.Domain.Models;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Infrastructure.Persistence.Adapters.Repository
{
    public class SpeciesRepository : GenericRepository<Species>, ISpeciesRepository
    {
        public SpeciesRepository(CuidandoPawsContext context) : base(context)
        {
            
        }

        public async Task<Species> GetLastAddedSpeciesAsync(CancellationToken cancellationToken)
        {
            var query = await _context.Set<Species>().AsQueryable()
                                .OrderByDescending(s => s.Id)
                                .FirstOrDefaultAsync(cancellationToken);
            return query;
        }

        public async Task<IEnumerable<Species>> GetOrderedByIdAsync(CancellationToken cancellationToken)
        {
            var query = await _context.Set<Species>().AsQueryable()
                    .OrderBy(s => s.Id)
                    .ToListAsync(cancellationToken);

            return query;
        }

        public async Task<IEnumerable<Species>> GetOrdereByIdDescSpeciesAsync(CancellationToken cancellationToken)
        {
            var query = await _context.Set<Species>().AsQueryable()
                .OrderByDescending(s => s.Id)
                .ToListAsync(cancellationToken);

            return query;
        }
    }
}
