using Warehouse.BLL.Models;

namespace Warehouse.BLL.Interfaces;
public interface IDeviceService : IGenericService<DeviceModel>
{
    public Task<DeviceModel> SetUpUserToDevice(Guid id, Guid? userId, CancellationToken cancellationToken);
}
