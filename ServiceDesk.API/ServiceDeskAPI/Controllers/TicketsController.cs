using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceDesk.BLL.Interfaces;
using ServiceDesk.BLL.Models;
using ServiceDesk.Domain.Enums;
using ServiceDeskAPI.Constants;
using ServiceDeskAPI.ViewModels;

namespace ServiceDeskAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;
    private readonly IMapper _mapper;

    public TicketsController(ITicketService ticketService, IMapper mapper)
    {
        _ticketService = ticketService;
        _mapper = mapper;
    }

    /// <summary>
    /// Return list of all tickets
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// GET /tickets
    /// </remarks>
    [HttpGet]
    public async Task<IEnumerable<TicketViewModel>> GetAllTickets(CancellationToken cancellationToken)
    {
        var ticketModels = await _ticketService.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<TicketViewModel>>(ticketModels);
    }

    /// <summary>
    /// Return tickets that created by user with certain id
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// GET /tickets/users/{id}
    /// </remarks>
    [HttpGet(EndpointsConstants.RequestWithUsersAndId)]
    public async Task<IEnumerable<TicketViewModel>> GetTicketsByUserId(Guid id, CancellationToken cancellationToken)
    {
        var ticketModels = await _ticketService.GetTicketsByUser(id, cancellationToken);
        return _mapper.Map<IEnumerable<TicketViewModel>>(ticketModels);
    }

    /// <summary>
    /// Delete ticket with certain id
    /// </summary>
    /// <param name="id">Ticket id</param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// DELETE /tickets/{id}
    /// </remarks>
    [HttpDelete(EndpointsConstants.RequestWithId)]
    public Task DeleteTicket(Guid id, CancellationToken cancellationToken)
    {
        return _ticketService.DeleteModelAsync(id, cancellationToken);
    }

    /// <summary>
    /// Update ticket with certain id
    /// </summary>
    /// <param name="id">Ticket id</param>
    /// <param name="ticket">Updated ticket</param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// PUT /tickets/{id}
    /// {
    ///     "name": "Laptop",
    ///     "description": "Gain a laptop",
    ///     "userId": "677df103-877f-4333-bb4e-5caa8bed4910",
    ///     "status": "InProgress",
    ///     "dateTimeAccepted": "2024-03-25"
    /// }
    /// </remarks>
    [HttpPut(EndpointsConstants.RequestWithId)]
    public async Task<TicketViewModel> UpdateTicket(Guid id, TicketUpdatingViewModel ticket, CancellationToken cancellationToken)
    {
        var ticketModel = _mapper.Map<TicketModel>(ticket);
        var newTicketModel = await _ticketService.UpdateModelAsync(id, ticketModel, cancellationToken);
        return _mapper.Map<TicketViewModel>(newTicketModel);
    }

    /// <summary>
    /// Create new ticket
    /// </summary>
    /// <param name="ticket">New ticket</param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// POST /tickets
    /// {
    ///     "name": "Laptop",
    ///     "description": "Gain a laptop",
    ///     "userId": "677df103-877f-4333-bb4e-5caa8bed4910",
    /// }
    /// </remarks>
    [HttpPost]
    public async Task<TicketViewModel> CreateTicket(TicketCreationViewModel ticket, CancellationToken cancellationToken)
    {
        var ticketModel = _mapper.Map<TicketModel>(ticket);
        var newTicketModel = await _ticketService.CreateModelAsync(ticketModel, cancellationToken);
        return _mapper.Map<TicketViewModel>(newTicketModel);
    }

    /// <summary>
    /// Update ticket status
    /// </summary>
    /// <param name="id">Ticket id</param>
    /// <param name="status">New ticket status</param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// PATCH /tickets/{id}
    /// {
    ///     "Status": "Free",
    /// }
    /// </remarks>
    [HttpPatch(EndpointsConstants.RequestWithId)]
    public async Task<TicketViewModel> UpdateTicketStatus(Guid id, Status status, CancellationToken cancellationToken)
    {
        var newTicketModel = await _ticketService.SetTicketStatus(id, status, cancellationToken);
        return _mapper.Map<TicketViewModel>(newTicketModel);
    }
}
