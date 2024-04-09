using MongoDB.Driver;
using Warehouse.DAL.Entities;

namespace Warehouse.DAL.Interfaces;
public interface IApplicationMongoDbContext<TEntity> where TEntity : Device
{
    IMongoCollection<TEntity> GetCollection();
}
