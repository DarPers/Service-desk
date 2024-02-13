using System.Linq.Expressions;
using ServiceDesk.DAL.Entities;

namespace ServiceDesk.DAL.GenericRepository;

internal interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<TEntity?> GetEntityByPredicateAsync(Func<TEntity, bool> predicate, CancellationToken cancellationToken);

    Task AddEntityAsync(TEntity entity, CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>> GetEntitiesByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    Task UpdateEntityAsync(TEntity entity, CancellationToken cancellationToken);

    Task DeleteEntityAsync(TEntity entity, CancellationToken cancellationToken);

    IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);

    IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties);
}
