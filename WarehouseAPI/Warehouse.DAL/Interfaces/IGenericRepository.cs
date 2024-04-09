using System.Linq.Expressions;
using Warehouse.DAL.Entities;

namespace Warehouse.DAL.Interfaces;
public interface IGenericRepository<TEntity> where TEntity : Device
{
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<TEntity> AddEntityAsync(TEntity entity, CancellationToken cancellationToken);

    Task<List<TEntity>> GetEntitiesByPredicateAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken);

    Task UpdateEntityAsync(TEntity entity, CancellationToken cancellationToken);

    Task DeleteEntityAsync(Guid id, CancellationToken cancellationToken);

    Task<TEntity> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken);
}
