using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Warehouse.BLL.Interfaces;
using Warehouse.BLL.Models;
using WarehouseAPI.Constants;

namespace WarehouseAPI.Controllers;
[Route("[controller]")]
[ApiController]
public class GenericApiController<TViewModel, TAddedViewModel, TUpdatedViewModel, TModel> : ControllerBase where TModel : DeviceModel
{
    private readonly IGenericService<TModel> _genericService;
    private readonly IMapper _mapper;

    public GenericApiController(IGenericService<TModel> genericService, IMapper mapper)
    {
        _genericService = genericService;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all devices
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// GET /devices
    /// </remarks>
    [HttpGet]
    public async Task<List<TViewModel>> GetAll(CancellationToken cancellationToken)
    {
        var models = await _genericService.GetAllModels(cancellationToken);
        return _mapper.Map<List<TViewModel>>(models);
    }

    /// <summary>
    /// Get device with certain id
    /// </summary>
    /// <param name="id">Device id</param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// GET /devices/{id}
    /// </remarks>
    [HttpGet(EndpointsConstants.RequestWithId)]
    public async Task<TViewModel> GetModelById(Guid id, CancellationToken cancellationToken)
    {
        var model = await _genericService.GetModelById(id, cancellationToken);
        return _mapper.Map<TViewModel>(model);
    }

    /// <summary>
    /// Get devices with certain name
    /// </summary>
    /// <param name="name">Device name</param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// GET /devices/{name}
    /// </remarks>
    [HttpGet(EndpointsConstants.RequestWithName)]
    public async Task<List<TViewModel>> GetModelByName(string name, CancellationToken cancellationToken)
    {
        var models = await _genericService.GetModelsByName(name, cancellationToken);
        return _mapper.Map<List<TViewModel>>(models);
    }

    /// <summary>
    /// Delete device with certain id
    /// </summary>
    /// <param name="id">Device id</param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// DELETE /devices/{id}
    /// </remarks>
    [HttpDelete(EndpointsConstants.RequestWithId)]
    public Task DeleteModelById(Guid id, CancellationToken cancellationToken)
    {
        return _genericService.DeleteModel(id, cancellationToken);
    }

    /// <summary>
    /// Update device with certain id
    /// </summary>
    /// <param name="id">Device id</param>
    /// <param name="updatedViewModel">Updated device</param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// PUT /devices/{id}
    /// {
    ///     "name": "Laptop Lenovo",
    ///     "serialNumber": "CNU1283W8D",
    ///     "modelName": "IdeaPad",
    ///     "characteristics": {"OS" : "Windows 10"},
    ///     "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///     "handedDateTime": "2024-04-05T13:52:21.644Z"
    /// }
    /// </remarks>
    [HttpPut(EndpointsConstants.RequestWithId)]
    public async Task<TViewModel> UpdateModel(Guid id, TUpdatedViewModel updatedViewModel, CancellationToken cancellationToken)
    {
        var updatedModel = _mapper.Map<TModel>(updatedViewModel);
        var model = await _genericService.UpdateModel(id, updatedModel, cancellationToken);
        return _mapper.Map<TViewModel>(model);
    }

    /// <summary>
    /// Add new device
    /// </summary>
    /// <param name="model">New device</param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// POST /devices
    /// {
    ///     "name": "Laptop Lenovo",
    ///     "serialNumber": "CNU1283W8D",
    ///     "modelName": "IdeaPad",
    ///     "characteristics": {"OS" : "Windows 10"},
    /// }
    /// </remarks>
    [HttpPost]
    public async Task<TViewModel> AddNewModel(TAddedViewModel model, CancellationToken cancellationToken)
    {
        var newModel = _mapper.Map<TModel>(model);
        var models = await _genericService.CreateModel(newModel, cancellationToken);
        return _mapper.Map<TViewModel>(models);
    }

}
