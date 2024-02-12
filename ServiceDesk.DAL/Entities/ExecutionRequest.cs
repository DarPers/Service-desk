using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServiceDesk.DAL.Entities
{
    internal class ExecutionRequest
    {
        public Guid Id { get; set; }

        public Guid TicketId { get; set; }

        public Ticket Ticket { get; set; } = null!;

        public Guid ExecutorId { get; set; }

        public User Executor { get; set; } = null!;
    }
}
