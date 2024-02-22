using AutoMapper;
using ServiceDesk.BLL.Interfaces;
using ServiceDesk.BLL.Models;
using ServiceDesk.DAL.Entities;
using ServiceDesk.DAL.GenericRepository;
using ServiceDesk.Domain.Enums;

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

        if (ticket == null)
        {
            throw new NullReferenceException("Ticket is null");
        }

        ticket.Status = newStatus;
        await _ticketRepository.UpdateEntityAsync(ticket, cancellationToken);
        return _mapper.Map<TicketModel>(ticket);
    }

    public override async Task<TicketModel> CreateModelAsync(TicketModel model, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetEntityByIdAsync(model.UserId, cancellationToken);

        if (user == null)
        {
            throw new NullReferenceException("User does not exist");
        }

        return await base.CreateModelAsync(model, cancellationToken);
    }
}
