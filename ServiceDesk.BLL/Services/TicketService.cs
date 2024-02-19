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

    public TicketService(IGenericRepository<Ticket> ticketRepository, IMapper mapper) 
        : base(ticketRepository, mapper)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<Status> GetTicketStatus(Guid id, CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetEntityByIdAsync(id, cancellationToken);

        return ticket?.Status ?? throw new NullReferenceException("Ticket is null");
    }

    public async Task SetTicketStatus(Guid id, Status newStatus, CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetEntityByIdAsync(id, cancellationToken);

        if (ticket == null)
        {
            return;
        }

        ticket.Status = newStatus;
        await _ticketRepository.UpdateEntityAsync(ticket, cancellationToken);
    }
}
