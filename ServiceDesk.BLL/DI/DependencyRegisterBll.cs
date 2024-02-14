using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceDesk.DAL.DI;

namespace ServiceDesk.BLL.DI;
public static class DependencyRegisterBll
{
    public static IServiceCollection AddBusinessLogicLevelServices(this IServiceCollection serviceCollection, 
        IConfiguration configuration)
    {
        serviceCollection.AddDataAccessLevelServices(configuration);

        return serviceCollection;
    }
}
