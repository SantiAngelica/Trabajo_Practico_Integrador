namespace Domain.Interfaces;

public interface IRepositoryBase<T>
    where T : class
{
    Task<T?> Create(T entity);
    Task<IReadOnlyList<T>> GetAll();
    Task<T?> GetById(string id);
    Task Update(string id, T entity);
    Task Delete(T entity);
    Task SaveChangesAsync();
}
