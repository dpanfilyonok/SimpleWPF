using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SimpleWPF.Repositories;

public class EntityRepository<TEntity, TKey> : ICrudRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>, new()
    where TKey : IEquatable<TKey>
{
    private readonly DbContext _context;

    public EntityRepository(DbContext context)
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

    public async Task DeleteAsync(TKey id)
    {
        var item = await GetAsync(id);
        if (item != null) _context.Set<TEntity>().Remove(item);
    }

    public Task UpdateAsync(TKey id, Expression<Func<TEntity, TEntity>> updateFactory)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}