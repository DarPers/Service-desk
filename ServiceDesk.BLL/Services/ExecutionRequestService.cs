using AutoMapper;
using ServiceDesk.BLL.Interfaces;
using ServiceDesk.BLL.Models;
using ServiceDesk.DAL.Entities;
using ServiceDesk.DAL.GenericRepository;

namespace ServiceDesk.BLL.Services;

public class ExecutionRequestService : GenericService<ExecutionRequestModel, ExecutionRequest>, IExecutionRequestService
{
    public ExecutionRequestService(IGenericRepository<ExecutionRequest> executionRequestRepository, IMapper mapper) 
        : base(executionRequestRepository, mapper)
    {
    }
}
