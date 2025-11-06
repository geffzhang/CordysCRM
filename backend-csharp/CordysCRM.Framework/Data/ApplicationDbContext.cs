using Microsoft.EntityFrameworkCore;
using CordysCRM.Framework.Domain;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Entity configurations will be applied when entities are registered
        // This allows the consuming project to add entities dynamically
    }

    /// <summary>
    /// Override SaveChanges to automatically set audit fields
    /// </summary>
    public override int SaveChanges()
    {
        SetAuditFields();
        return base.SaveChanges();
    }

    /// <summary>
    /// Override SaveChangesAsync to automatically set audit fields
    /// </summary>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetAuditFields()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is Framework.Domain.BaseModel && 
                       (e.State == EntityState.Added || e.State == EntityState.Modified));

        var currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        foreach (var entry in entries)
        {
            var entity = (Framework.Domain.BaseModel)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreateTime = currentTime;
                // entity.CreateUser should be set from the current user context
            }

            entity.UpdateTime = currentTime;
            // entity.UpdateUser should be set from the current user context
        }
    }
}
