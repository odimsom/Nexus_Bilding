using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class RoundingMethodRepository : IRoundingMethodRepository
{
    private readonly NexusBillingDbContext _context;

    public RoundingMethodRepository(NexusBillingDbContext context)
    {
        _context = context;
    }

    public async Task<RoundingMethod?> GetByIdAsync(long id)
    {
        return await _context.Set<RoundingMethod>().FindAsync(id);
    }

    public async Task<IEnumerable<RoundingMethod>> GetAllAsync()
    {
        return await _context.Set<RoundingMethod>().ToListAsync();
    }

    public async Task AddAsync(RoundingMethod entity)
    {
        await _context.Set<RoundingMethod>().AddAsync(entity);
    }

    public Task UpdateAsync(RoundingMethod entity)
    {
        _context.Set<RoundingMethod>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(RoundingMethod entity)
    {
        _context.Set<RoundingMethod>().Remove(entity);
        return Task.CompletedTask;
    }
}
