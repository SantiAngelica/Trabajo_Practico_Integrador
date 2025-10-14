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
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<bool> Delete(string id)
    {
        var entity = await GetById(id);
        if (entity == null)
            return false;
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public virtual async Task<IReadOnlyList<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<T?> GetById(string id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual async Task<T?> Update(string id, T entity)
    {
        var existingEntity = await GetById(id);
        if (existingEntity == null)
            return null;

        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return existingEntity;
    }
}
