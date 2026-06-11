using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class GeneralPostingSetupRepository : IGeneralPostingSetupRepository
{
    private readonly NexusBillingDbContext _context;

    public GeneralPostingSetupRepository(NexusBillingDbContext context)
    {
        _context = context;
    }

    public async Task<GeneralPostingSetup?> GetByIdAsync(long id)
    {
        return await _context.Set<GeneralPostingSetup>().FindAsync(id);
    }

    public async Task<IEnumerable<GeneralPostingSetup>> GetAllAsync()
    {
        return await _context.Set<GeneralPostingSetup>().ToListAsync();
    }

    public async Task AddAsync(GeneralPostingSetup entity)
    {
        await _context.Set<GeneralPostingSetup>().AddAsync(entity);
    }

    public Task UpdateAsync(GeneralPostingSetup entity)
    {
        _context.Set<GeneralPostingSetup>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(GeneralPostingSetup entity)
    {
        _context.Set<GeneralPostingSetup>().Remove(entity);
        return Task.CompletedTask;
    }
}
