using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class GLBudgetNameRepository : IGLBudgetNameRepository
{
    private readonly NexusBillingDbContext _context;

    public GLBudgetNameRepository(NexusBillingDbContext context)
    {
        _context = context;
    }

    public async Task<GLBudgetName?> GetByIdAsync(long id)
    {
        return await _context.Set<GLBudgetName>().FindAsync(id);
    }

    public async Task<IEnumerable<GLBudgetName>> GetAllAsync()
    {
        return await _context.Set<GLBudgetName>().ToListAsync();
    }

    public async Task AddAsync(GLBudgetName entity)
    {
        await _context.Set<GLBudgetName>().AddAsync(entity);
    }

    public Task UpdateAsync(GLBudgetName entity)
    {
        _context.Set<GLBudgetName>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(GLBudgetName entity)
    {
        _context.Set<GLBudgetName>().Remove(entity);
        return Task.CompletedTask;
    }
}
