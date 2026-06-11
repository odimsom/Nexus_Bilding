using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class GenProductPostingGroupRepository : IGenProductPostingGroupRepository
{
    private readonly NexusBillingDbContext _context;

    public GenProductPostingGroupRepository(NexusBillingDbContext context)
    {
        _context = context;
    }

    public async Task<GenProductPostingGroup?> GetByIdAsync(long id)
    {
        return await _context.Set<GenProductPostingGroup>().FindAsync(id);
    }

    public async Task<IEnumerable<GenProductPostingGroup>> GetAllAsync()
    {
        return await _context.Set<GenProductPostingGroup>().ToListAsync();
    }

    public async Task AddAsync(GenProductPostingGroup entity)
    {
        await _context.Set<GenProductPostingGroup>().AddAsync(entity);
    }

    public Task UpdateAsync(GenProductPostingGroup entity)
    {
        _context.Set<GenProductPostingGroup>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(GenProductPostingGroup entity)
    {
        _context.Set<GenProductPostingGroup>().Remove(entity);
        return Task.CompletedTask;
    }
}
