using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleWPF.Data;

namespace SimpleWPF.Repositories;

public class EntityRepository<TEntity, TKey> : ICrudRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>, new()
    where TKey : IEquatable<TKey>
{
    private readonly ApplicationContext _context;

    public EntityRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public IQueryable<TEntity> GetAll()
    {
        return _context.Set<TEntity>().AsNoTracking();
    }

    public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
    {
        return GetAll().Where(predicate);
    }

    public async Task<TEntity?> GetAsync(TKey id)
    {
        return await _context.FindAsync<TEntity>(id);
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async Task<TKey> AddAsync(TEntity item)
    {
        await _context.AddAsync(item);
        await _context.SaveChangesAsync();
        return item.Id;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}