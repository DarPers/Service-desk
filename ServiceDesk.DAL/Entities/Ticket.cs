using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ServiceDesk.DAL.Enums;

namespace ServiceDesk.DAL.Entities
{
    internal class Ticket
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; } = null!;

        public Status Status { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeAccepted { get; set; }
    }
}
