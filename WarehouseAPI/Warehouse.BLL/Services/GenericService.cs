using AutoMapper;
using Warehouse.BLL.Interfaces;
using Warehouse.BLL.Models;
using Warehouse.DAL.Entities;
using Warehouse.DAL.Interfaces;
using WarehouseAPI.Shared.Exceptions;

namespace Warehouse.BLL.Services;
public class GenericService<TModel, TEntity> : IGenericService<TModel> where TModel : DeviceModel where TEntity : Device
{
    private readonly IGenericRepository<TEntity> _genericRepository;
    private readonly IMapper _mapper;

    public GenericService(IGenericRepository<TEntity> genericRepository, IMapper mapper)
    {
        _genericRepository = genericRepository;
        _mapper = mapper;
    }

    public async Task<TModel> CreateModel(TModel model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TEntity>(model);
        var addedEntity = await _genericRepository.AddEntityAsync(entity, cancellationToken);
        return _mapper.Map<TModel>(addedEntity);
    }

    public Task DeleteModel(Guid id, CancellationToken cancellationToken)
    {
        return _genericRepository.DeleteEntityAsync(id, cancellationToken);
    }

    public async Task<List<TModel>> GetAllModels(CancellationToken cancellationToken)
    {
        var entities = await _genericRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<TModel>>(entities);
    }

    public async Task<List<TModel>> GetModelsByName(string name, CancellationToken cancellationToken)
    {
        var entities = await _genericRepository.GetEntitiesByPredicateAsync(i => i.Name.ToLower().Contains(name.ToLower()), cancellationToken);
        return _mapper.Map<List<TModel>>(entities);
    }

    public async Task<TModel> GetModelById(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _genericRepository.GetEntityByIdAsync(id, cancellationToken);
        return _mapper.Map<TModel>(entity);
    }

    public async Task<TModel> UpdateModel(Guid id, TModel model, CancellationToken cancellationToken)
    {
        var entity = await _genericRepository.GetEntityByIdAsync(id, cancellationToken);

        if (entity == null)
        {
            throw new NullEntityException("Device does not exist!");
        }

        model.Id = id;
        var updatedEntity = _mapper.Map<TEntity>(model);
        await _genericRepository.UpdateEntityAsync(updatedEntity, cancellationToken);
        return model;
    }
}
