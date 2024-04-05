using ServiceDesk.BLL.Models;
using ServiceDesk.DAL.Entities;
using ServiceDesk.Domain.Enums;

namespace ServiceDesk.BLL.Interfaces;

public interface ITicketService : IGenericService<TicketModel, Ticket>
{
    public Task<TicketModel> SetTicketStatus(Guid id, Status newStatus, CancellationToken cancellationToken);

    public Task<Status> GetTicketStatus(Guid id, CancellationToken cancellationToken);

    public Task<IEnumerable<TicketModel>> GetTicketsByUser(Guid id,
        CancellationToken cancellationToken);
}
