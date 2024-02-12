using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ServiceDesk.DAL.Enums;

namespace ServiceDesk.DAL.Entities
{
    internal class Ticket
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public User Customer { get; set; }

        [Required]
        public Status Status { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeAccepted { get; set; }
    }
}
