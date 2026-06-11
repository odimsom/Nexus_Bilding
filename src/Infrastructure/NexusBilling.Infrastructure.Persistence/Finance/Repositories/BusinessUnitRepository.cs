using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class BusinessUnitRepository : IBusinessUnitRepository
{
    private readonly NexusBillingDbContext _context;

    public BusinessUnitRepository(NexusBillingDbContext context)
    {
        _context = context;
    }

    public async Task<BusinessUnit?> GetByIdAsync(long id)
    {
        return await _context.Set<BusinessUnit>().FindAsync(id);
    }

    public async Task<IEnumerable<BusinessUnit>> GetAllAsync()
    {
        return await _context.Set<BusinessUnit>().ToListAsync();
    }

    public async Task AddAsync(BusinessUnit entity)
    {
        await _context.Set<BusinessUnit>().AddAsync(entity);
    }

    public Task UpdateAsync(BusinessUnit entity)
    {
        _context.Set<BusinessUnit>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(BusinessUnit entity)
    {
        _context.Set<BusinessUnit>().Remove(entity);
        return Task.CompletedTask;
    }
}
