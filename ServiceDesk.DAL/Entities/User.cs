using System.ComponentModel.DataAnnotations;
using ServiceDesk.DAL.Enums;

namespace ServiceDesk.DAL.Entities
{
    internal class User
    {
        [Key]
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}
