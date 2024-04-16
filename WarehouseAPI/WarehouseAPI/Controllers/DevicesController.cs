using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Warehouse.BLL.Interfaces;
using Warehouse.BLL.Models;
using WarehouseAPI.Constants;
using WarehouseAPI.ViewModels.Device;

namespace WarehouseAPI.Controllers;
[Route("[controller]")]
[ApiController]
public class DevicesController : GenericApiController<DeviceViewModel, DeviceAddedViewModel, DeviceUpdatedViewModel, DeviceModel>
{
    private readonly IDeviceService _deviceService;
    private readonly IMapper _mapper;

    public DevicesController(IDeviceService deviceService, IMapper mapper) : base(deviceService, mapper)
    {
        _deviceService = deviceService;
        _mapper = mapper;
    }

    /// <summary>
    /// Update device with certain id
    /// </summary>
    /// <param name="id">Device id</param>
    /// <param name="userId">ID of user, who got a device</param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// PATCH /devices/{id}
    /// </remarks>
    [HttpPatch(EndpointsConstants.RequestWithId)]
    public async Task<DeviceViewModel> SetUserToDevice(Guid id, Guid? userId, CancellationToken cancellationToken)
    {
        var model = await _deviceService.SetUpUserToDevice(id, userId, cancellationToken);
        return _mapper.Map<DeviceViewModel>(model);
    }
}
