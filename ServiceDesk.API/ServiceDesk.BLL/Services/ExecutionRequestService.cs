using AutoMapper;
using ServiceDesk.BLL.Interfaces;
using ServiceDesk.BLL.Models;
using ServiceDesk.DAL.Entities;
using ServiceDesk.DAL.GenericRepository;
using ServiceDesk.Domain.Exceptions;

namespace ServiceDesk.BLL.Services;

public class ExecutionRequestService : GenericService<ExecutionRequestModel, ExecutionRequest>, IExecutionRequestService
{
    private readonly IGenericRepository<Ticket> _ticketRepository;
    private readonly IGenericRepository<User> _userRepository;
    private readonly IGenericRepository<ExecutionRequest> _executionRequestRepository;
    private readonly IMapper _mapper;

    public ExecutionRequestService(IGenericRepository<ExecutionRequest> executionRequestRepository,
        IGenericRepository<Ticket> ticketRepository, IGenericRepository<User> userRepository, IMapper mapper) 
        : base(executionRequestRepository, mapper)
    {
        _executionRequestRepository = executionRequestRepository;
        _ticketRepository = ticketRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public override async Task<ExecutionRequestModel> CreateModelAsync(ExecutionRequestModel model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(model);

        var ticket = await _ticketRepository.GetEntityByIdAsync(model.TicketId, cancellationToken);
        EntityIsNullException.ThrowIfNull(ticket);

        var executor = await _userRepository.GetEntityByIdAsync(model.ExecutorId, cancellationToken);
        EntityIsNullException.ThrowIfNull(executor);

        return await base.CreateModelAsync(model, cancellationToken);
    }

    public async Task<IEnumerable<ExecutionRequestModel>> GetExecutionRequestsByExecutor(Guid id, CancellationToken cancellationToken)
    {
        var executionRequests = await _executionRequestRepository.GetEntitiesByPredicateAsync(p => p.ExecutorId == id, cancellationToken);
        return _mapper.Map<IEnumerable<ExecutionRequestModel>>(executionRequests);
    }

    public async Task<IEnumerable<ExecutionRequestModel>> GetExecutionRequestsByTicket(Guid id, CancellationToken cancellationToken)
    {
        var executionRequests = await _executionRequestRepository.GetEntitiesByPredicateAsync(p => p.TicketId == id, cancellationToken);
        return _mapper.Map<IEnumerable<ExecutionRequestModel>>(executionRequests);
    }
}
