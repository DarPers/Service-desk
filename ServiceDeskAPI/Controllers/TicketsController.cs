using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceDesk.BLL.Interfaces;
using ServiceDesk.BLL.Models;
using ServiceDesk.Domain.Enums;
using ServiceDeskAPI.ViewModels;

namespace ServiceDeskAPI.Controllers
{
    [Route("api/tickets")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;

        TicketsController(ITicketService ticketService, IMapper mapper)
        {
            _ticketService = ticketService;
            _mapper = mapper;
        }

        [HttpGet("/all")]
        public async Task<IActionResult> GetAllTickets(CancellationToken cancellationToken)
        {
            var ticketModels = await _ticketService.GetAllAsync(cancellationToken);
            var ticketViewModels = _mapper.Map<IEnumerable<TicketViewModel>>(ticketModels);
            return Ok(ticketViewModels);
        }

        [HttpGet("/users/{id:Guid}")]
        public async Task<IActionResult> GetTicketsByUserId(Guid id, CancellationToken cancellationToken)
        {
            var ticketModels = await _ticketService.GetListByPredicateAsync(p => p.UserId == id, cancellationToken);
            var ticketViewModels = _mapper.Map<TicketViewModel>(ticketModels);
            return Ok(ticketViewModels);
        }

        [HttpDelete("/{id:Guid}")]
        public async Task<IActionResult> DeleteTicket(Guid id, CancellationToken cancellationToken)
        {
            await _ticketService.DeleteModelAsync(id, cancellationToken);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTicket(TicketViewModel ticket, CancellationToken cancellationToken)
        {
            var ticketModel = _mapper.Map<TicketModel>(ticket);
            await _ticketService.UpdateModelAsync(ticketModel, cancellationToken);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(TicketCreationViewModel ticket, CancellationToken cancellationToken)
        {
            var ticketModel = _mapper.Map<TicketModel>(ticket);
            await _ticketService.CreateModelAsync(ticketModel, cancellationToken);
            return Ok();
        }

        [HttpPatch("{id:Guid}/status")]
        public async Task<IActionResult> UpdateTicketStatus(Guid id, Status status, CancellationToken cancellationToken)
        {
            await _ticketService.SetTicketStatus(id, status, cancellationToken);
            return Ok();
        }
    }
}
