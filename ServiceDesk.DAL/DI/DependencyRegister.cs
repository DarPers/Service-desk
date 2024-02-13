using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceDesk.DAL.Data;

namespace ServiceDesk.DAL.DI;

public static class DependencyRegister
{
    public static IServiceCollection AddDataAccessLevelServices(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(option =>
            option.UseNpgsql(configuration.GetConnectionString("PostgresSQLConnectionString")));

        return serviceCollection;
    }
}
