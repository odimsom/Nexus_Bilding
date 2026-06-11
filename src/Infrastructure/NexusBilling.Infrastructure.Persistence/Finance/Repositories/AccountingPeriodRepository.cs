using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class AccountingPeriodRepository : IAccountingPeriodRepository
{
    private readonly NexusBillingDbContext _context;

    public AccountingPeriodRepository(NexusBillingDbContext context)
    {
        _context = context;
    }

    public async Task<AccountingPeriod?> GetByIdAsync(long id)
    {
        return await _context.Set<AccountingPeriod>().FindAsync(id);
    }

    public async Task<IEnumerable<AccountingPeriod>> GetAllAsync()
    {
        return await _context.Set<AccountingPeriod>().ToListAsync();
    }

    public async Task AddAsync(AccountingPeriod entity)
    {
        await _context.Set<AccountingPeriod>().AddAsync(entity);
    }

    public Task UpdateAsync(AccountingPeriod entity)
    {
        _context.Set<AccountingPeriod>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(AccountingPeriod entity)
    {
        _context.Set<AccountingPeriod>().Remove(entity);
        return Task.CompletedTask;
    }
}
