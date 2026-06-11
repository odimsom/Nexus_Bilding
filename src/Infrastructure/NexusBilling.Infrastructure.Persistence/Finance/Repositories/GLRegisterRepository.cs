using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class GLRegisterRepository : IGLRegisterRepository
{
    private readonly NexusBillingDbContext _context;

    public GLRegisterRepository(NexusBillingDbContext context)
    {
        _context = context;
    }

    public async Task<GLRegister?> GetByIdAsync(long id)
    {
        return await _context.Set<GLRegister>().FindAsync(id);
    }

    public async Task<IEnumerable<GLRegister>> GetAllAsync()
    {
        return await _context.Set<GLRegister>().ToListAsync();
    }

    public async Task AddAsync(GLRegister entity)
    {
        await _context.Set<GLRegister>().AddAsync(entity);
    }

    public Task UpdateAsync(GLRegister entity)
    {
        _context.Set<GLRegister>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(GLRegister entity)
    {
        _context.Set<GLRegister>().Remove(entity);
        return Task.CompletedTask;
    }
}
