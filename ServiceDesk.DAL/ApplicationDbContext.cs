using Microsoft.EntityFrameworkCore;
using ServiceDesk.DAL.Entities;

namespace ServiceDesk.DAL;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Ticket> Tickets { get; set; }

    public DbSet<ExecutionRequest> ExecutionRequests { get; set; }

    public override int SaveChanges()
    {
        var modifiedEntities = ChangeTracker.Entries()
            .Where(i => i.State == EntityState.Modified || i.State == EntityState.Added);

        foreach (var modifiedEntity in modifiedEntities)
        {
            var entity = (BaseEntity)modifiedEntity.Entity;

            switch (modifiedEntity.State)
            {
                case EntityState.Added:
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.UpdatedAt = DateTime.UtcNow; 
                    break;

                case EntityState.Modified:
                    entity.UpdatedAt = DateTime.UtcNow;
                    modifiedEntity.Property("CreatedAt").IsModified = false;
                    break;
            }
        }

        return base.SaveChanges();
    }
}