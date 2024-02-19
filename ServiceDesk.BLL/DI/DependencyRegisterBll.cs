using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceDesk.BLL.Interfaces;
using ServiceDesk.BLL.Mapping;
using ServiceDesk.BLL.Services;
using ServiceDesk.DAL.DI;

namespace ServiceDesk.BLL.DI;
public static class DependencyRegisterBll
{
    public static IServiceCollection AddBusinessLogicLevelServices(this IServiceCollection serviceCollection, 
        IConfiguration configuration)
    {
        serviceCollection.AddDataAccessLevelServices(configuration);
        serviceCollection.AddAutoMapper(typeof(MappingProfile));
        serviceCollection.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));

        return serviceCollection;
    }
}
