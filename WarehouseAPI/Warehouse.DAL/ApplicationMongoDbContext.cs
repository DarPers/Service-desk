using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Warehouse.DAL.Attributes;
using Warehouse.DAL.Entities;
using Warehouse.DAL.Interfaces;

namespace Warehouse.DAL;
public class ApplicationMongoDbContext<TEntity> : IApplicationMongoDbContext<TEntity> where TEntity : Device
{
    private readonly IMongoDatabase _database;

    public ApplicationMongoDbContext(IConfiguration configuration)
    {
        var settings = MongoClientSettings.FromConnectionString(configuration.GetConnectionString("MongoDbConnection"));
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        var mongodbClient = new MongoClient(settings);
        _database = mongodbClient.GetDatabase(configuration.GetConnectionString("DatabaseName"));
    }

    public IMongoCollection<TEntity> GetCollection()
    {
        return _database.GetCollection<TEntity>((typeof(TEntity).GetCustomAttributes(typeof(BsonCollectionAttribute), true)
            .FirstOrDefault() as BsonCollectionAttribute)?
            .CollectionName);
    }
}
