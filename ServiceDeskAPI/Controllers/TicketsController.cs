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

    [HttpGet("users/" + EndpointsConstants.RequestWithGuidId)]
    public async Task<IEnumerable<TicketViewModel>> GetTicketsByUserId(Guid id, CancellationToken cancellationToken)
    {
        var ticketModels = await _ticketService.GetListByPredicateAsync(p => p.UserId == id, cancellationToken);
        return _mapper.Map<IEnumerable<TicketViewModel>>(ticketModels);
    }

    [HttpDelete(EndpointsConstants.RequestWithGuidId)]
    public void DeleteTicket(Guid id, CancellationToken cancellationToken)
    {
        _ticketService.DeleteModelAsync(id, cancellationToken);
    }

    [HttpPut(EndpointsConstants.RequestWithGuidId)]
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

    [HttpPatch(EndpointsConstants.RequestWithGuidId)]
    public async Task<TicketViewModel> UpdateTicketStatus(Guid id, Status status, CancellationToken cancellationToken)
    {
        var newTicketModel = await _ticketService.SetTicketStatus(id, status, cancellationToken);
        return _mapper.Map<TicketViewModel>(newTicketModel);
    }
}
