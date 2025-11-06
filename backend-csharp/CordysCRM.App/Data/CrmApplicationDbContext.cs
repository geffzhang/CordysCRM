using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CordysCRM.Framework.Data;
using CordysCRM.Framework.Security;
using CordysCRM.CRM.Domain;

namespace CordysCRM.App.Data;

/// <summary>
/// Extended ApplicationDbContext that includes CRM entities and Identity
/// Since we need Identity, this actually needs to extend IdentityDbContext
/// </summary>
public class CrmApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public CrmApplicationDbContext(DbContextOptions<CrmApplicationDbContext> options) 
        : base(options)
    {
    }

    // DbSets for CRM entities
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Clue> Clues { get; set; }
    public DbSet<Opportunity> Opportunities { get; set; }
    
    // System module entities
    public DbSet<Department> Departments { get; set; }
    public new DbSet<Role> Roles { get; set; }  // Use 'new' to hide Identity's Roles DbSet
    public DbSet<ModuleField> ModuleFields { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure Identity tables with custom names (optional)
        modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");
        modelBuilder.Entity<ApplicationRole>().ToTable("AspNetRoles");
        
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

        // Configure Clue entity
        modelBuilder.Entity<Clue>(entity =>
        {
            entity.ToTable("clue");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Owner).HasMaxLength(50);
            entity.Property(e => e.Stage).HasMaxLength(50);
            entity.Property(e => e.LastStage).HasMaxLength(50);
            entity.Property(e => e.Contact).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Products).HasMaxLength(500);
            entity.Property(e => e.OrganizationId).HasMaxLength(50);
            entity.Property(e => e.TransitionType).HasMaxLength(20);
            entity.Property(e => e.TransitionId).HasMaxLength(50);
            entity.Property(e => e.Follower).HasMaxLength(50);
            entity.Property(e => e.PoolId).HasMaxLength(50);
            entity.Property(e => e.ReasonId).HasMaxLength(50);
            entity.Property(e => e.CreateUser).HasMaxLength(50);
            entity.Property(e => e.UpdateUser).HasMaxLength(50);
        });

        // Configure Opportunity entity
        modelBuilder.Entity<Opportunity>(entity =>
        {
            entity.ToTable("opportunity");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.CustomerId).HasMaxLength(50);
            entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Possible).HasColumnType("decimal(5,2)");
            entity.Property(e => e.Products).HasMaxLength(500);
            entity.Property(e => e.OrganizationId).HasMaxLength(50);
            entity.Property(e => e.LastStage).HasMaxLength(50);
            entity.Property(e => e.Stage).HasMaxLength(50);
            entity.Property(e => e.ContactId).HasMaxLength(50);
            entity.Property(e => e.Owner).HasMaxLength(50);
            entity.Property(e => e.Follower).HasMaxLength(50);
            entity.Property(e => e.FailureReason).HasMaxLength(500);
            entity.Property(e => e.CreateUser).HasMaxLength(50);
            entity.Property(e => e.UpdateUser).HasMaxLength(50);
        });

        // Configure Department entity
        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("sys_department");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.OrganizationId).HasMaxLength(50);
            entity.Property(e => e.ParentId).HasMaxLength(50);
            entity.Property(e => e.Resource).HasMaxLength(50);
            entity.Property(e => e.ResourceId).HasMaxLength(100);
            entity.Property(e => e.CreateUser).HasMaxLength(50);
            entity.Property(e => e.UpdateUser).HasMaxLength(50);
        });

        // Configure Role entity
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("sys_role");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.DataScope).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.OrganizationId).HasMaxLength(50);
            entity.Property(e => e.CreateUser).HasMaxLength(50);
            entity.Property(e => e.UpdateUser).HasMaxLength(50);
        });

        // Configure ModuleField entity
        modelBuilder.Entity<ModuleField>(entity =>
        {
            entity.ToTable("sys_module_field");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FormId).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.InternalKey).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.CreateUser).HasMaxLength(50);
            entity.Property(e => e.UpdateUser).HasMaxLength(50);
        });
    }
}
