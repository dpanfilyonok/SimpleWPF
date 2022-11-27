using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SimpleWPF.Repositories;

public interface IReadOnlyRepository<TEntity, in TKey> 
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
    
    Task<TEntity?> GetAsync(TKey id);
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate);
}