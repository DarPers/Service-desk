using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceDesk.DAL.Data;
using ServiceDesk.DAL.Entities;
using ServiceDesk.DAL.GenericRepository;

namespace ServiceDesk.DAL.DI;

public static class DependencyRegister
{
    public static IServiceCollection AddDataAccessLevelServices(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(option =>
            option.UseNpgsql(configuration.GetConnectionString("PostgresSQLConnectionString")));

        serviceCollection.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
        serviceCollection.AddScoped<IGenericRepository<Ticket>, GenericRepository<Ticket>>();
        serviceCollection.AddScoped<IGenericRepository<ExecutionRequest>, GenericRepository<ExecutionRequest>>();

        return serviceCollection;
    }
}
