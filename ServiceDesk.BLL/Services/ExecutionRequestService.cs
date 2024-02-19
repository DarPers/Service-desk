using AutoMapper;
using ServiceDesk.BLL.Models;
using ServiceDesk.DAL.Entities;
using ServiceDesk.DAL.GenericRepository;

namespace ServiceDesk.BLL.Services;

public class ExecutionRequestService : GenericService<ExecutionRequestModel, ExecutionRequest>
{
    public ExecutionRequestService(IGenericRepository<ExecutionRequest> executionRequestRepository, IMapper mapper) 
        : base(executionRequestRepository, mapper)
    {
    }
}
