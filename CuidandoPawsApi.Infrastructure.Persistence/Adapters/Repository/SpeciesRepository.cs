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

        public async Task<Species> GetLastAddedSpeciesAsync(DateTime entryOfSpeciesDate)
        {
            var query = await _context.Set<Species>().AsQueryable()
                                .OrderByDescending(s => s.EntryOfSpecie == entryOfSpeciesDate)
                                .FirstOrDefaultAsync();
            return query;
        }
    }
}
