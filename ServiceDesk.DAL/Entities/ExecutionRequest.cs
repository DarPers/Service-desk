using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServiceDesk.DAL.Entities
{
    internal class ExecutionRequest
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid TicketId { get; set; }

        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }

        [Required]
        public Guid ExecutorId { get; set; }

        [ForeignKey("ExecutorId")]
        public User Executor { get; set; }
    }
}
