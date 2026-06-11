using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class TrialBalanceSetupRepository : ITrialBalanceSetupRepository
{
    private readonly NexusBillingDbContext _context;

    public TrialBalanceSetupRepository(NexusBillingDbContext context)
    {
        _context = context;
    }

    public async Task<TrialBalanceSetup?> GetByIdAsync(long id)
    {
        return await _context.Set<TrialBalanceSetup>().FindAsync(id);
    }

    public async Task<IEnumerable<TrialBalanceSetup>> GetAllAsync()
    {
        return await _context.Set<TrialBalanceSetup>().ToListAsync();
    }

    public async Task AddAsync(TrialBalanceSetup entity)
    {
        await _context.Set<TrialBalanceSetup>().AddAsync(entity);
    }

    public Task UpdateAsync(TrialBalanceSetup entity)
    {
        _context.Set<TrialBalanceSetup>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TrialBalanceSetup entity)
    {
        _context.Set<TrialBalanceSetup>().Remove(entity);
        return Task.CompletedTask;
    }
}
