using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceDesk.BLL.Interfaces;
using ServiceDesk.BLL.Models;
using ServiceDeskAPI.Constants;
using ServiceDeskAPI.ViewModels;

namespace ServiceDeskAPI.Controllers;
[Route("[controller]")]
[ApiController]
public class ExecutionRequestsController : ControllerBase
{
    private readonly IExecutionRequestService _executionRequestService;
    private readonly IMapper _mapper;

    public ExecutionRequestsController(IExecutionRequestService executionRequestService, IMapper mapper)
    {
        _executionRequestService = executionRequestService;
        _mapper = mapper;
    }

    [HttpGet(EndpointsConstants.RequestWithExecutorAndId)]
    public async Task<IEnumerable<ExecutionRequestViewModel>> GetExecutionRequestsByExecutor(Guid id, CancellationToken cancellationToken)
    {
        var executionRequestModels =
            await _executionRequestService.GetExecutionRequestsByExecutor(id, cancellationToken);
        return _mapper.Map<IEnumerable<ExecutionRequestViewModel>>(executionRequestModels);
    }

    [HttpGet(EndpointsConstants.RequestWithTicketAndId)]
    public async Task<IEnumerable<ExecutionRequestViewModel>> GetExecutionRequestsByTicket(Guid id, CancellationToken cancellationToken)
    {
        var executionRequestModels = await _executionRequestService.GetExecutionRequestsByTicket(id, cancellationToken);
        return _mapper.Map<IEnumerable<ExecutionRequestViewModel>>(executionRequestModels);
    }

    [HttpGet(EndpointsConstants.RequestWithId)]
    public async Task<ExecutionRequestViewModel> GetExecutionRequestById(Guid id, CancellationToken cancellationToken)
    {
        var executionRequestModels = await _executionRequestService.GetModelByIdAsync(id, cancellationToken);
        return _mapper.Map<ExecutionRequestViewModel>(executionRequestModels);
    }

    [HttpDelete(EndpointsConstants.RequestWithId)]
    public Task DeleteExecutionRequest(Guid id, CancellationToken cancellationToken)
    {
        return _executionRequestService.DeleteModelAsync(id, cancellationToken);
    }

    [HttpPut(EndpointsConstants.RequestWithId)]
    public async Task<ExecutionRequestViewModel> UpdateExecutionRequest(Guid id, ExecutionRequestUpdatingViewModel executionRequest, CancellationToken cancellationToken)
    {
        var executionRequestModel = _mapper.Map<ExecutionRequestModel>(executionRequest);
        var newExecutionRequestModel = await _executionRequestService.UpdateModelAsync(id, executionRequestModel, cancellationToken);
        return _mapper.Map<ExecutionRequestViewModel>(newExecutionRequestModel);
    }

    [HttpPost]
    public async Task<ExecutionRequestViewModel> CreateExecutionRequest(ExecutionRequestCreationViewModel executionRequest, CancellationToken cancellationToken)
    {
        var executionRequestModel = _mapper.Map<ExecutionRequestModel>(executionRequest);
        var newExecutionRequestModel = await _executionRequestService.CreateModelAsync(executionRequestModel, cancellationToken);
        return _mapper.Map<ExecutionRequestViewModel>(newExecutionRequestModel);
    }
}