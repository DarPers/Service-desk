using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.DAL.Interfaces;
using Warehouse.DAL.Repositories;

namespace Warehouse.DAL.DI;
public static class DependencyRegister
{
    public static IServiceCollection AddDataAccessLevelServices(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddScoped(typeof(IApplicationMongoDbContext<>), typeof(ApplicationMongoDbContext<>));
        serviceCollection.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        return serviceCollection;
    }
}
