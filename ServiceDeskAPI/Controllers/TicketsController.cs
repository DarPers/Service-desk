using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceDesk.BLL.Interfaces;
using ServiceDesk.BLL.Models;
using ServiceDesk.Domain.Enums;
using ServiceDeskAPI.ViewModels;

namespace ServiceDeskAPI.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("all")]
        public async Task<IEnumerable<TicketViewModel>> GetAllTickets(CancellationToken cancellationToken)
        {
            var ticketModels = await _ticketService.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<TicketViewModel>>(ticketModels);
        }

        [HttpGet("users/{id:Guid}")]
        public async Task<IEnumerable<TicketViewModel>> GetTicketsByUserId(Guid id, CancellationToken cancellationToken)
        {
            var ticketModels = await _ticketService.GetListByPredicateAsync(p => p.UserId == id, cancellationToken);
            return _mapper.Map<IEnumerable<TicketViewModel>>(ticketModels);
        }

        [HttpDelete("{id:Guid}")]
        public async Task DeleteTicket(Guid id, CancellationToken cancellationToken)
        {
            await _ticketService.DeleteModelAsync(id, cancellationToken);
        }

        [HttpPut("{id:Guid}")]
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

        [HttpPatch("{id:Guid}/status")]
        public async Task<TicketViewModel> UpdateTicketStatus(Guid id, Status status, CancellationToken cancellationToken)
        {
            var newTicketModel = await _ticketService.SetTicketStatus(id, status, cancellationToken);
            return _mapper.Map<TicketViewModel>(newTicketModel);
        }
    }
}
