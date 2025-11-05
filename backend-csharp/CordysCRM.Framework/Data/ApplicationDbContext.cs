using Microsoft.EntityFrameworkCore;

namespace CordysCRM.Framework.Data;

/// <summary>
/// Application database context for Entity Framework Core
/// This is the main DbContext for the CordysCRM application
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets will be added here as entities are migrated
    // Example:
    // public DbSet<User> Users { get; set; }
    // public DbSet<Customer> Customers { get; set; }
    // public DbSet<Opportunity> Opportunities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Entity configurations will be added here
        // Example:
        // modelBuilder.Entity<User>(entity => {
        //     entity.ToTable("users");
        //     entity.HasKey(e => e.Id);
        //     entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
        // });
    }
}
