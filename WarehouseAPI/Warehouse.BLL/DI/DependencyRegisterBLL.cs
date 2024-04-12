using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.BLL.Interfaces;
using Warehouse.BLL.Mapping;
using Warehouse.BLL.Models;
using Warehouse.BLL.Services;
using Warehouse.DAL.DI;
using Warehouse.DAL.Entities;

namespace Warehouse.BLL.DI;
public static class DependencyRegisterBll
{
    public static IServiceCollection AddBusinessLogicLevelServices(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDataAccessLevelServices(configuration);
        serviceCollection.AddScoped(typeof(IGenericService<DeviceModel>), typeof(GenericService<DeviceModel, Device>));
        serviceCollection.AddScoped<IDeviceService, DeviceService>();
        serviceCollection.AddAutoMapper(typeof(MappingProfile));
        return serviceCollection;
    }
}
