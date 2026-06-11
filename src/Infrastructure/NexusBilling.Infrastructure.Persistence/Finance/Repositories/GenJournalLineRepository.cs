using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class GenJournalLineRepository : IGenJournalLineRepository
{
    private readonly NexusBillingDbContext _context;

    public GenJournalLineRepository(NexusBillingDbContext context)
    {
        _context = context;
    }

    public async Task<GenJournalLine?> GetByIdAsync(long id)
    {
        return await _context.Set<GenJournalLine>().FindAsync(id);
    }

    public async Task<IEnumerable<GenJournalLine>> GetAllAsync()
    {
        return await _context.Set<GenJournalLine>().ToListAsync();
    }

    public async Task AddAsync(GenJournalLine entity)
    {
        await _context.Set<GenJournalLine>().AddAsync(entity);
    }

    public Task UpdateAsync(GenJournalLine entity)
    {
        _context.Set<GenJournalLine>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(GenJournalLine entity)
    {
        _context.Set<GenJournalLine>().Remove(entity);
        return Task.CompletedTask;
    }
}
