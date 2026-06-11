using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class GLAccountCategoryRepository : IGLAccountCategoryRepository
{
    private readonly NexusBillingDbContext _context;

    public GLAccountCategoryRepository(NexusBillingDbContext context)
    {
        _context = context;
    }

    public async Task<GLAccountCategory?> GetByIdAsync(long id)
    {
        return await _context.Set<GLAccountCategory>().FindAsync(id);
    }

    public async Task<IEnumerable<GLAccountCategory>> GetAllAsync()
    {
        return await _context.Set<GLAccountCategory>().ToListAsync();
    }

    public async Task AddAsync(GLAccountCategory entity)
    {
        await _context.Set<GLAccountCategory>().AddAsync(entity);
    }

    public Task UpdateAsync(GLAccountCategory entity)
    {
        _context.Set<GLAccountCategory>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(GLAccountCategory entity)
    {
        _context.Set<GLAccountCategory>().Remove(entity);
        return Task.CompletedTask;
    }
}
