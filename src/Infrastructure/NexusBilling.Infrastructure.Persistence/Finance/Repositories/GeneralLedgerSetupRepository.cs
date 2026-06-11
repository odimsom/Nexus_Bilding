using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class GeneralLedgerSetupRepository : IGeneralLedgerSetupRepository
{
    private readonly NexusBillingDbContext _context;

    public GeneralLedgerSetupRepository(NexusBillingDbContext context)
    {
        _context = context;
    }

    public async Task<GeneralLedgerSetup?> GetByIdAsync(long id)
    {
        return await _context.Set<GeneralLedgerSetup>().FindAsync(id);
    }

    public async Task<IEnumerable<GeneralLedgerSetup>> GetAllAsync()
    {
        return await _context.Set<GeneralLedgerSetup>().ToListAsync();
    }

    public async Task AddAsync(GeneralLedgerSetup entity)
    {
        await _context.Set<GeneralLedgerSetup>().AddAsync(entity);
    }

    public Task UpdateAsync(GeneralLedgerSetup entity)
    {
        _context.Set<GeneralLedgerSetup>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(GeneralLedgerSetup entity)
    {
        _context.Set<GeneralLedgerSetup>().Remove(entity);
        return Task.CompletedTask;
    }
}
