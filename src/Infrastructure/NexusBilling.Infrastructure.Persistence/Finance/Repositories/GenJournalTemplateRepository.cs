using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class GenJournalTemplateRepository : IGenJournalTemplateRepository
{
    private readonly NexusBillingDbContext _context;

    public GenJournalTemplateRepository(NexusBillingDbContext context)
    {
        _context = context;
    }

    public async Task<GenJournalTemplate?> GetByIdAsync(long id)
    {
        return await _context.Set<GenJournalTemplate>().FindAsync(id);
    }

    public async Task<IEnumerable<GenJournalTemplate>> GetAllAsync()
    {
        return await _context.Set<GenJournalTemplate>().ToListAsync();
    }

    public async Task AddAsync(GenJournalTemplate entity)
    {
        await _context.Set<GenJournalTemplate>().AddAsync(entity);
    }

    public Task UpdateAsync(GenJournalTemplate entity)
    {
        _context.Set<GenJournalTemplate>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(GenJournalTemplate entity)
    {
        _context.Set<GenJournalTemplate>().Remove(entity);
        return Task.CompletedTask;
    }
}
