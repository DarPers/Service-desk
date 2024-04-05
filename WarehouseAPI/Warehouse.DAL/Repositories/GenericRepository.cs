using MongoDB.Driver;
using System.Linq.Expressions;
using Warehouse.DAL.Entities;
using Warehouse.DAL.Interfaces;

namespace Warehouse.DAL.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Device
{
    private readonly IMongoCollection<TEntity> _collection;

    public GenericRepository(IMongoDatabase mongoDatabase)
    {
        _collection = mongoDatabase.GetCollection<TEntity>(typeof(TEntity).Name);
    }

    public async Task<TEntity> AddEntityAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(entity, cancellationToken);
        return entity;
    }

    public Task DeleteEntityAsync(Guid id, CancellationToken cancellationToken)
    {
        return _collection.DeleteOneAsync(i => i.Id == id, cancellationToken);
    }

    public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        var filter = Builders<TEntity>.Filter.Empty;
        return _collection.Find(filter).ToListAsync(cancellationToken);
    }

    public Task<List<TEntity>> GetEntitiesByPredicateAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        var filter = Builders<TEntity>.Filter.Where(predicate);
        return _collection.Find(filter).ToListAsync(cancellationToken);
    }

    public Task<TEntity> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _collection.Find(i => i.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public Task UpdateEntityAsync(TEntity entity, CancellationToken cancellationToken)
    {
        return _collection.ReplaceOneAsync(i => i.Id == entity.Id, entity);
    }
}
