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

    [HttpGet]
    public async Task<IEnumerable<TicketViewModel>> GetAllTickets(CancellationToken cancellationToken)
    {
        var ticketModels = await _ticketService.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<TicketViewModel>>(ticketModels);
    }

    [HttpGet(EndpointsConstants.RequestWithUsersAndId)]
    public async Task<IEnumerable<TicketViewModel>> GetTicketsByUserId(Guid id, CancellationToken cancellationToken)
    {
        var ticketModels = await _ticketService.GetTicketsByUser(id, cancellationToken);
        return _mapper.Map<IEnumerable<TicketViewModel>>(ticketModels);
    }

    [HttpDelete(EndpointsConstants.RequestWithId)]
    public Task DeleteTicket(Guid id, CancellationToken cancellationToken)
    {
        return _ticketService.DeleteModelAsync(id, cancellationToken);
    }

    [HttpPut(EndpointsConstants.RequestWithId)]
    public async Task<TicketViewModel> UpdateTicket(Guid id, TicketUpdatingViewModel ticket, CancellationToken cancellationToken)
    {
        var ticketModel = _mapper.Map<TicketModel>(ticket);
        var newTicketModel = await _ticketService.UpdateModelAsync(id, ticketModel, cancellationToken);
        return _mapper.Map<TicketViewModel>(newTicketModel);
    }

    [HttpPost]
    public async Task<TicketViewModel> CreateTicket(TicketCreationViewModel ticket, CancellationToken cancellationToken)
    {
        var ticketModel = _mapper.Map<TicketModel>(ticket);
        var newTicketModel = await _ticketService.CreateModelAsync(ticketModel, cancellationToken);
        return _mapper.Map<TicketViewModel>(newTicketModel);
    }

    [HttpPatch(EndpointsConstants.RequestWithId)]
    public async Task<TicketViewModel> UpdateTicketStatus(Guid id, Status status, CancellationToken cancellationToken)
    {
        var newTicketModel = await _ticketService.SetTicketStatus(id, status, cancellationToken);
        return _mapper.Map<TicketViewModel>(newTicketModel);
    }
}
