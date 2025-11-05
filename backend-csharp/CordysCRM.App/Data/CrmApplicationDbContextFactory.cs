using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using CordysCRM.Framework.Data;

namespace CordysCRM.App.Data;

/// <summary>
/// Design-time factory for EF Core migrations
/// </summary>
public class CrmApplicationDbContextFactory : IDesignTimeDbContextFactory<CrmApplicationDbContext>
{
    public CrmApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CrmApplicationDbContext>();
        
        // Use a default connection string for migrations
        // This will be overridden at runtime
        var connectionString = "Server=localhost;Port=3306;Database=cordys_crm;Uid=root;Pwd=password;";
        
        optionsBuilder.UseMySql(
            connectionString, 
            new MySqlServerVersion(new Version(8, 0, 21)),  // Use a specific version instead of AutoDetect
            b => b.MigrationsAssembly("CordysCRM.App"));

        return new CrmApplicationDbContext(optionsBuilder.Options);
    }
}
