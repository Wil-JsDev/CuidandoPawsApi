using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Infrastructure.Persistence.Adapters.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CuidandoPawsContext _context;

        public GenericRepository(CuidandoPawsContext context)
        {
            _context = context;
        }

        public virtual async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
          await _context.Set<T>().AddAsync(entity, cancellationToken);
          await SaveAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            T? t = await _context.Set<T>().FindAsync(id, cancellationToken);
            await SaveAsync();
            return t;
        }

        public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            _context.Set<T>().Remove(entity);
            await SaveAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken) => 
            await _context.Set<T>().ToListAsync(cancellationToken);


        public virtual async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await SaveAsync();
        }

       public virtual async Task SaveAsync() => 
            await _context.SaveChangesAsync();
        
    }
}
