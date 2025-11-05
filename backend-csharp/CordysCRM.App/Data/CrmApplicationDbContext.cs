using Microsoft.EntityFrameworkCore;
using CordysCRM.Framework.Data;
using CordysCRM.CRM.Domain;

namespace CordysCRM.App.Data;

/// <summary>
/// Extended ApplicationDbContext that includes CRM entities
/// </summary>
public class CrmApplicationDbContext : ApplicationDbContext
{
    public CrmApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    // DbSets for CRM entities
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure Customer entity
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("customer");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Owner).HasMaxLength(50);
            entity.Property(e => e.OrganizationId).HasMaxLength(50);
            entity.Property(e => e.PoolId).HasMaxLength(50);
            entity.Property(e => e.Follower).HasMaxLength(50);
            entity.Property(e => e.ReasonId).HasMaxLength(50);
            entity.Property(e => e.CreateUser).HasMaxLength(50);
            entity.Property(e => e.UpdateUser).HasMaxLength(50);
        });
    }
}
