using System.ComponentModel.DataAnnotations;
using ServiceDesk.DAL.Enums;

namespace ServiceDesk.DAL.Entities
{
    internal class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public Role Role { get; set; }
    }
}
