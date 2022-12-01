using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SimpleWPF.Repositories;

public interface ICrudRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    Task<TKey> AddAsync(TEntity item);
    Task DeleteAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task SaveAsync();
}