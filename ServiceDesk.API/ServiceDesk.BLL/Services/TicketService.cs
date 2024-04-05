using AutoMapper;
using ServiceDesk.BLL.Interfaces;
using ServiceDesk.BLL.Models;
using ServiceDesk.DAL.Entities;
using ServiceDesk.DAL.GenericRepository;
using ServiceDesk.Domain.Enums;
using ServiceDesk.Domain.Exceptions;

namespace ServiceDesk.BLL.Services;

public class TicketService : GenericService<TicketModel, Ticket>, ITicketService
{
    private readonly IGenericRepository<Ticket> _ticketRepository;
    private readonly IGenericRepository<User> _userRepository;
    private readonly IMessageService _massageService;
    private readonly IMapper _mapper;

    public TicketService(IGenericRepository<Ticket> ticketRepository, IGenericRepository<User> userRepository, IMapper mapper, IMessageService massageService)
        : base(ticketRepository, mapper)
    {
        _ticketRepository = ticketRepository;
        _userRepository = userRepository;
        _massageService = massageService;
        _mapper = mapper;
    }

    public async Task<Status> GetTicketStatus(Guid id, CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetEntityByIdAsync(id, cancellationToken);

        EntityIsNullException.ThrowIfNull(ticket);

        return ticket!.Status;
    }

    public async Task<TicketModel> SetTicketStatus(Guid id, Status newStatus, CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetEntityByIdAsync(id, cancellationToken);

        EntityIsNullException.ThrowIfNull(ticket);

        ticket.Status = newStatus;
        await _ticketRepository.UpdateEntityAsync(ticket, cancellationToken);

        var user = await _userRepository.GetEntityByIdAsync(ticket.UserId, cancellationToken);

        EntityIsNullException.ThrowIfNull(user);

        await _massageService.SendCommandChangedStatus(ticket, user.Email);

        return _mapper.Map<TicketModel>(ticket);
    }

    public override async Task<TicketModel> CreateModelAsync(TicketModel model, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetEntityByIdAsync(model.UserId, cancellationToken);

        EntityIsNullException.ThrowIfNull(user);

        var emails = await GetExecutorsEmails(cancellationToken);

        await _massageService.SendCommandTicketAdded(model, emails);

        return await base.CreateModelAsync(model, cancellationToken);
    }

    public async Task<IEnumerable<TicketModel>> GetTicketsByUser(Guid id, CancellationToken cancellationToken)
    {
        var ticketEntities = await _ticketRepository.GetEntitiesByPredicateAsync(p => p.UserId == id, cancellationToken);
        return _mapper.Map<IEnumerable<TicketModel>>(ticketEntities);
    }

    private async Task<List<string>> GetExecutorsEmails(CancellationToken cancellationToken)
    {
        var executors = await _userRepository.GetEntitiesByPredicateAsync(i => i.Role == Role.OperationAdministrator || i.Role == Role.SystemAdministrator, cancellationToken);
        return executors.Select(executor => executor.Email).ToList();
    }
}
