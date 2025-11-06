using CordysCRM.CRM.Domain;
using CordysCRM.Framework.Data;
using CordysCRM.Framework.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CordysCRM.CRM.Repositories;

/// <summary>
/// Module field repository implementation
/// </summary>
public class ModuleFieldRepository : Repository<ModuleField>, IModuleFieldRepository
{
    public ModuleFieldRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<ModuleField>> GetByFormIdAsync(string formId)
    {
        return await _dbSet
            .Where(f => f.FormId == formId)
            .OrderBy(f => f.Pos)
            .ToListAsync();
    }

    public async Task<ModuleField?> GetByInternalKeyAsync(string internalKey)
    {
        return await _dbSet
            .FirstOrDefaultAsync(f => f.InternalKey == internalKey);
    }
}
