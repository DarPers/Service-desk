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

    /// <summary>
    /// Get execution requests that were accepted by executor with certain id
    /// </summary>
    /// <param name="id">Executor id</param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// GET /executionrequests/executors/{id}
    /// </remarks>
    [HttpGet(EndpointsConstants.RequestWithExecutorAndId)]
    public async Task<IEnumerable<ExecutionRequestViewModel>> GetExecutionRequestsByExecutor(Guid id, CancellationToken cancellationToken)
    {
        var executionRequestModels =
            await _executionRequestService.GetExecutionRequestsByExecutor(id, cancellationToken);
        return _mapper.Map<IEnumerable<ExecutionRequestViewModel>>(executionRequestModels);
    }

    /// <summary>
    /// Get execution requests that were created for ticket with certain id
    /// </summary>
    /// <param name="id">Ticket id</param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// GET /executionrequests/tickets/{id}
    /// </remarks>
    [HttpGet(EndpointsConstants.RequestWithTicketAndId)]
    public async Task<IEnumerable<ExecutionRequestViewModel>> GetExecutionRequestsByTicket(Guid id, CancellationToken cancellationToken)
    {
        var executionRequestModels = await _executionRequestService.GetExecutionRequestsByTicket(id, cancellationToken);
        return _mapper.Map<IEnumerable<ExecutionRequestViewModel>>(executionRequestModels);
    }

    /// <summary>
    /// Get execution request with certain id
    /// </summary>
    /// <param name="id">Execution request id</param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// GET /executionrequests/{id}
    /// </remarks>
    [HttpGet(EndpointsConstants.RequestWithId)]
    public async Task<ExecutionRequestViewModel> GetExecutionRequestById(Guid id, CancellationToken cancellationToken)
    {
        var executionRequestModels = await _executionRequestService.GetModelByIdAsync(id, cancellationToken);
        return _mapper.Map<ExecutionRequestViewModel>(executionRequestModels);
    }

    /// <summary>
    /// Delete execution request with certain id
    /// </summary>
    /// <param name="id">Execution request id</param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// DELETE /executionrequests/{id}
    /// </remarks>
    [HttpDelete(EndpointsConstants.RequestWithId)]
    public Task DeleteExecutionRequest(Guid id, CancellationToken cancellationToken)
    {
        return _executionRequestService.DeleteModelAsync(id, cancellationToken);
    }

    /// <summary>
    /// Update execution request with certain id
    /// </summary>
    /// <param name="id">Execution request id</param>
    /// <param name="executionRequest">Updated execution request</param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// PUT /executionrequests/{id}
    /// {
    ///     "ticketId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///     "executorId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    /// }
    /// </remarks>
    [HttpPut(EndpointsConstants.RequestWithId)]
    public async Task<ExecutionRequestViewModel> UpdateExecutionRequest(Guid id, ExecutionRequestUpdatingViewModel executionRequest, CancellationToken cancellationToken)
    {
        var executionRequestModel = _mapper.Map<ExecutionRequestModel>(executionRequest);
        var newExecutionRequestModel = await _executionRequestService.UpdateModelAsync(id, executionRequestModel, cancellationToken);
        return _mapper.Map<ExecutionRequestViewModel>(newExecutionRequestModel);
    }

    /// <summary>
    /// Create execution request
    /// </summary>
    /// <param name="executionRequest">New execution request</param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// PUT /executionrequests/{id}
    /// {
    ///     "ticketId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///     "executorId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    /// }
    /// </remarks>
    [HttpPost]
    public async Task<ExecutionRequestViewModel> CreateExecutionRequest(ExecutionRequestCreationViewModel executionRequest, CancellationToken cancellationToken)
    {
        var executionRequestModel = _mapper.Map<ExecutionRequestModel>(executionRequest);
        var newExecutionRequestModel = await _executionRequestService.CreateModelAsync(executionRequestModel, cancellationToken);
        return _mapper.Map<ExecutionRequestViewModel>(newExecutionRequestModel);
    }
}