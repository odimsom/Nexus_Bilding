using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class GLEntryRepository : IGLEntryRepository
{
    private readonly NexusBillingDbContext _context;

    public GLEntryRepository(NexusBillingDbContext context)
    {
        _context = context;
    }

    public async Task<GLEntry?> GetByIdAsync(long id)
    {
        return await _context.Set<GLEntry>().FindAsync(id);
    }

    public async Task<IEnumerable<GLEntry>> GetAllAsync()
    {
        return await _context.Set<GLEntry>().ToListAsync();
    }

    public async Task AddAsync(GLEntry entity)
    {
        await _context.Set<GLEntry>().AddAsync(entity);
    }

    public Task UpdateAsync(GLEntry entity)
    {
        _context.Set<GLEntry>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(GLEntry entity)
    {
        _context.Set<GLEntry>().Remove(entity);
        return Task.CompletedTask;
    }
}
