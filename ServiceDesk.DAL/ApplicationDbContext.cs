using Microsoft.EntityFrameworkCore;
using ServiceDesk.DAL.Entities;

namespace ServiceDesk.DAL.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
            
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Ticket> Tickets { get; set; }

    public DbSet<ExecutionRequest> ExecutionRequests { get; set; }
}