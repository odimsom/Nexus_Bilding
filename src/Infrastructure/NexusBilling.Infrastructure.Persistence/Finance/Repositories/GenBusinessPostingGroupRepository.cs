using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class GenBusinessPostingGroupRepository : IGenBusinessPostingGroupRepository
{
    private readonly NexusBillingDbContext _context;

    public GenBusinessPostingGroupRepository(NexusBillingDbContext context)
    {
        _context = context;
    }

    public async Task<GenBusinessPostingGroup?> GetByIdAsync(long id)
    {
        return await _context.Set<GenBusinessPostingGroup>().FindAsync(id);
    }

    public async Task<IEnumerable<GenBusinessPostingGroup>> GetAllAsync()
    {
        return await _context.Set<GenBusinessPostingGroup>().ToListAsync();
    }

    public async Task AddAsync(GenBusinessPostingGroup entity)
    {
        await _context.Set<GenBusinessPostingGroup>().AddAsync(entity);
    }

    public Task UpdateAsync(GenBusinessPostingGroup entity)
    {
        _context.Set<GenBusinessPostingGroup>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(GenBusinessPostingGroup entity)
    {
        _context.Set<GenBusinessPostingGroup>().Remove(entity);
        return Task.CompletedTask;
    }
}
