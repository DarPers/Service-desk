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
    private readonly IMapper _mapper;

    public TicketService(IGenericRepository<Ticket> ticketRepository, IGenericRepository<User> userRepository, IMapper mapper) 
        : base(ticketRepository, mapper)
    {
        _ticketRepository = ticketRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Status> GetTicketStatus(Guid id, CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetEntityByIdAsync(id, cancellationToken);

        return ticket?.Status ?? throw new NullReferenceException("Ticket is null");
    }

    public async Task<TicketModel> SetTicketStatus(Guid id, Status newStatus, CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetEntityByIdAsync(id, cancellationToken);

        EntityIsNullException.ThrowIfNull(ticket);

        ticket.Status = newStatus;
        await _ticketRepository.UpdateEntityAsync(ticket, cancellationToken);
        return _mapper.Map<TicketModel>(ticket);
    }

    public override async Task<TicketModel> CreateModelAsync(TicketModel model, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetEntityByIdAsync(model.UserId, cancellationToken);

        EntityIsNullException.ThrowIfNull(user);

        return await base.CreateModelAsync(model, cancellationToken);
    }

    public async Task<IEnumerable<TicketModel>> GetTicketsByUser(Guid id, CancellationToken cancellationToken)
    {
        var ticketEntities = await _ticketRepository.GetEntitiesByPredicateAsync(p => p.UserId == id, cancellationToken);
        return _mapper.Map<IEnumerable<TicketModel>>(ticketEntities);
    }
}
