using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class StandardGeneralJournalLineRepository : IStandardGeneralJournalLineRepository
{
    private readonly NexusBillingDbContext _context;

    public StandardGeneralJournalLineRepository(NexusBillingDbContext context)
    {
        _context = context;
    }

    public async Task<StandardGeneralJournalLine?> GetByIdAsync(long id)
    {
        return await _context.Set<StandardGeneralJournalLine>().FindAsync(id);
    }

    public async Task<IEnumerable<StandardGeneralJournalLine>> GetAllAsync()
    {
        return await _context.Set<StandardGeneralJournalLine>().ToListAsync();
    }

    public async Task AddAsync(StandardGeneralJournalLine entity)
    {
        await _context.Set<StandardGeneralJournalLine>().AddAsync(entity);
    }

    public Task UpdateAsync(StandardGeneralJournalLine entity)
    {
        _context.Set<StandardGeneralJournalLine>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(StandardGeneralJournalLine entity)
    {
        _context.Set<StandardGeneralJournalLine>().Remove(entity);
        return Task.CompletedTask;
    }
}
