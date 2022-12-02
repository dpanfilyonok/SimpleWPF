using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleWPF.Data;

namespace SimpleWPF.Repositories;

public class EntityRepository<TEntity, TKey> : ICrudRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>, new()
    where TKey : IEquatable<TKey>
{
    private readonly ILogger<EntityRepository<TEntity, TKey>> _logger;
    private readonly ApplicationContext _context;

    public EntityRepository(ILogger<EntityRepository<TEntity, TKey>> logger, ApplicationContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    public IQueryable<TEntity> GetAll()
    {
        _logger.LogInformation("Getting all entities");
        return _context.Set<TEntity>().AsNoTracking();
    }

    public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
    {
        return GetAll().Where(predicate);
    }

    public async Task<TEntity?> GetAsync(TKey id)
    {
        _logger.LogInformation("Getting entity by key {Id}", id);
        return await _context.FindAsync<TEntity>(id);
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async Task<TKey> AddAsync(TEntity entity)
    {
        _logger.LogInformation("Adding entity {Entity}", entity);
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        _context.Entry(entity).State = EntityState.Detached;
        return entity.Id;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _logger.LogInformation("Removing entity {Entity}", entity);
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
        _context.Entry(entity).State = EntityState.Detached;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _logger.LogInformation("Updating entity {Entity}", entity);
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
        _context.Entry(entity).State = EntityState.Detached;
    }
}