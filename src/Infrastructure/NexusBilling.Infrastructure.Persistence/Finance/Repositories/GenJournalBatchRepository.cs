using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class GenJournalBatchRepository : IGenJournalBatchRepository
{
    private readonly NexusBillingDbContext _context;

    public GenJournalBatchRepository(NexusBillingDbContext context)
    {
        _context = context;
    }

    public async Task<GenJournalBatch?> GetByIdAsync(long id)
    {
        return await _context.Set<GenJournalBatch>().FindAsync(id);
    }

    public async Task<IEnumerable<GenJournalBatch>> GetAllAsync()
    {
        return await _context.Set<GenJournalBatch>().ToListAsync();
    }

    public async Task AddAsync(GenJournalBatch entity)
    {
        await _context.Set<GenJournalBatch>().AddAsync(entity);
    }

    public Task UpdateAsync(GenJournalBatch entity)
    {
        _context.Set<GenJournalBatch>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(GenJournalBatch entity)
    {
        _context.Set<GenJournalBatch>().Remove(entity);
        return Task.CompletedTask;
    }
}
