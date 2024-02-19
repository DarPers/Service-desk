using System.Linq.Expressions;
using ServiceDesk.BLL.Models;
using ServiceDesk.DAL.Entities;

namespace ServiceDesk.BLL.Interfaces;

public interface IGenericService<TModel, TEntity> where TModel : BaseModel where TEntity : BaseEntity
{
    public Task CreateModelAsync(TModel model, CancellationToken cancellationToken);

    public Task DeleteModelAsync(Guid id, CancellationToken cancellationToken);

    public Task<TModel?> GetModelByIdAsync(Guid id, CancellationToken cancellationToken);

    public Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken);

    public Task<IEnumerable<TModel>> GetListByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    public Task UpdateModelAsync(TModel model, CancellationToken cancellationToken);
}
