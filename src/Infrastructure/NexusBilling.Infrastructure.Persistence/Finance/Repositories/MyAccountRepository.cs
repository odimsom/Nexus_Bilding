using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class MyAccountRepository : IMyAccountRepository
{
    private readonly NexusBillingDbContext _context;

    public MyAccountRepository(NexusBillingDbContext context)
    {
        _context = context;
    }

    public async Task<MyAccount?> GetByIdAsync(long id)
    {
        return await _context.Set<MyAccount>().FindAsync(id);
    }

    public async Task<IEnumerable<MyAccount>> GetAllAsync()
    {
        return await _context.Set<MyAccount>().ToListAsync();
    }

    public async Task AddAsync(MyAccount entity)
    {
        await _context.Set<MyAccount>().AddAsync(entity);
    }

    public Task UpdateAsync(MyAccount entity)
    {
        _context.Set<MyAccount>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(MyAccount entity)
    {
        _context.Set<MyAccount>().Remove(entity);
        return Task.CompletedTask;
    }
}
