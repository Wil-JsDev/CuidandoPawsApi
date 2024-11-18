namespace CuidandoPawsApi.Domain.Ports.Repository;

public interface IGenericRepository<T> where T : class
{
    Task AddAsync(T entity, CancellationToken cancellationToken);

    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);

    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task DeleteAsync (T entity, CancellationToken cancellationToken);

    Task UpdateAsync (T entity);

    Task SaveAsync();
}