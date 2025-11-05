using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class RepositoryBase<T> : IRepositoryBase<T>
    where T : class
{
    protected readonly DbContext _context;

    public RepositoryBase(DbContext context)
    {
        _context = context;
    }

    public virtual async Task<T?> Create(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public virtual async Task Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public virtual async Task<IReadOnlyList<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<T?> GetById(int id)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
    }

    public virtual async Task Update(int id, T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
