namespace CuidandoPawsApi.Domain.Ports.Repository;

public interface IGenericRepository<T> where T : class
{
    Task AddAsync(T entity);

    Task<IEnumerable<T>> GetAllAsync();

    Task<T> GetByIdAsync(int id);

    Task DeleteAsync (int id);

    Task UpdateAsync (int id, T entity);
}