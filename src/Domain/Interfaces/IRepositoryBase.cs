namespace Domain.Interfaces;

public interface IRepositoryBase<T>
    where T : class
{
    Task<T?> Create(T entity);
    Task<IReadOnlyList<T>> GetAll();
    Task<T?> GetById(string id);
    Task<T?> Update(string id, T entity);
    Task<bool> Delete(string id);
}
