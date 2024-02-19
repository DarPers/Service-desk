using AutoMapper;
using ServiceDesk.BLL.Interfaces;
using ServiceDesk.BLL.Models;
using ServiceDesk.DAL.Entities;
using ServiceDesk.DAL.GenericRepository;
using System.Linq.Expressions;

namespace ServiceDesk.BLL.Services;
public class GenericService<TModel, TEntity> : IGenericService<TModel, TEntity> where TModel: BaseModel where TEntity : BaseEntity
{
    private readonly IGenericRepository<TEntity> _genericRepository;
    private readonly IMapper _mapper;

    public GenericService(IGenericRepository<TEntity> genericRepository, IMapper mapper)
    {
        _genericRepository = genericRepository;
        _mapper = mapper;
    }

    public async Task CreateModelAsync(TModel model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TEntity>(model);
        await _genericRepository.AddEntityAsync(entity, cancellationToken);
    }

    public async Task DeleteModelAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _genericRepository.GetEntityByIdAsync(id, cancellationToken);

        if (entity != null)
        {
            await _genericRepository.DeleteEntityAsync(entity, cancellationToken);
        }
    }

    public async Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await _genericRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<TModel>>(entities);
    }

    public async Task<IEnumerable<TModel>> GetListWhereAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        var entities = await _genericRepository.GetEntitiesByPredicateAsync(predicate, cancellationToken);
        return _mapper.Map<IEnumerable<TModel>>(entities);
    }

    public async Task<TModel?> GetModelByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _genericRepository.GetEntityByIdAsync(id, cancellationToken);

        if (entity == null)
        {
            return null;
        }

        return _mapper.Map<TModel>(entity);
    }

    public async Task UpdateModelAsync(TModel model, CancellationToken cancellationToken)
    {
        var entity = await _genericRepository.GetEntityByIdAsync(model.Id, cancellationToken);

        if (entity == null)
        {
            return;
        }

        var newEntity = _mapper.Map<TEntity>(model);
        await _genericRepository.UpdateEntityAsync(newEntity, cancellationToken);
    }
}
