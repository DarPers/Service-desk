using ServiceDesk.BLL.Models;
using ServiceDesk.DAL.Entities;

namespace ServiceDesk.BLL.Interfaces;

public interface IExecutionRequestService : IGenericService<ExecutionRequestModel, ExecutionRequest>
{
    public Task<IEnumerable<ExecutionRequestModel>> GetExecutionRequestsByExecutor(Guid id,
        CancellationToken cancellationToken);

    public Task<IEnumerable<ExecutionRequestModel>> GetExecutionRequestsByTicket(Guid id,
        CancellationToken cancellationToken);
}
