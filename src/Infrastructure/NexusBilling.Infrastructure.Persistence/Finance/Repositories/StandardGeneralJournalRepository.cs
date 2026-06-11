using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class StandardGeneralJournalRepository : IStandardGeneralJournalRepository
{
    private readonly NexusBillingDbContext _context;

    public StandardGeneralJournalRepository(NexusBillingDbContext context)
    {
        _context = context;
    }

    public async Task<StandardGeneralJournal?> GetByIdAsync(long id)
    {
        return await _context.Set<StandardGeneralJournal>().FindAsync(id);
    }

    public async Task<IEnumerable<StandardGeneralJournal>> GetAllAsync()
    {
        return await _context.Set<StandardGeneralJournal>().ToListAsync();
    }

    public async Task AddAsync(StandardGeneralJournal entity)
    {
        await _context.Set<StandardGeneralJournal>().AddAsync(entity);
    }

    public Task UpdateAsync(StandardGeneralJournal entity)
    {
        _context.Set<StandardGeneralJournal>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(StandardGeneralJournal entity)
    {
        _context.Set<StandardGeneralJournal>().Remove(entity);
        return Task.CompletedTask;
    }
}
