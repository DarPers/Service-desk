using AutoMapper;
using Warehouse.BLL.Interfaces;
using Warehouse.BLL.Models;
using Warehouse.DAL.Entities;
using Warehouse.DAL.Interfaces;

namespace Warehouse.BLL.Services;
public class DeviceService : GenericService<DeviceModel, Device>, IDeviceService
{
    private readonly IGenericRepository<Device> _genericRepository;
    private readonly IMapper _mapper;

    public DeviceService(IGenericRepository<Device> genericRepository, IMapper mapper) : base(genericRepository, mapper)
    {
        _genericRepository = genericRepository;
        _mapper = mapper;
    }

    public async Task<DeviceModel> SetUpUserToDevice(Guid id, Guid? userId, CancellationToken cancellationToken)
    {
        var device = await _genericRepository.GetEntityByIdAsync(id, cancellationToken);

        if (device == null)
        {
            throw new NullReferenceException("Device does not exist!");
        }

        if (userId != null)
        {
            device.UserId = userId;
            device.HandedDateTime = DateTime.UtcNow;
        }
        else
        {
            device.UserId = null;
            device.HandedDateTime = null;
        }

        await _genericRepository.UpdateEntityAsync(device, cancellationToken);

        return _mapper.Map<DeviceModel>(device);
    }
}
