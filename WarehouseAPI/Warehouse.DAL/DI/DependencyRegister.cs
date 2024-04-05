using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Warehouse.DAL.Interfaces;
using Warehouse.DAL.Repositories;

namespace Warehouse.DAL.DI;
public static class DependencyRegister
{
    public static IServiceCollection AddDataAccessLevelServices(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        serviceCollection.AddScoped(_ =>
        {
            var connectionString = configuration.GetSection("MongodbSettings:ConnectionString").Value;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var mongodbClient = new MongoClient(settings);
            return mongodbClient.GetDatabase(configuration["MongodbSettings:DatabaseName"]);
        });

        return serviceCollection;
    }
}
