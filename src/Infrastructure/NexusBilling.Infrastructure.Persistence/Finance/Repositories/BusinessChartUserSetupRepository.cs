using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class BusinessChartUserSetupRepository : IBusinessChartUserSetupRepository
{
    private readonly NexusBillingDbContext _context;

    public BusinessChartUserSetupRepository(NexusBillingDbContext context)
    {
        _context = context;
    }

    public async Task<BusinessChartUserSetup?> GetByIdAsync(long id)
    {
        return await _context.Set<BusinessChartUserSetup>().FindAsync(id);
    }

    public async Task<IEnumerable<BusinessChartUserSetup>> GetAllAsync()
    {
        return await _context.Set<BusinessChartUserSetup>().ToListAsync();
    }

    public async Task AddAsync(BusinessChartUserSetup entity)
    {
        await _context.Set<BusinessChartUserSetup>().AddAsync(entity);
    }

    public Task UpdateAsync(BusinessChartUserSetup entity)
    {
        _context.Set<BusinessChartUserSetup>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(BusinessChartUserSetup entity)
    {
        _context.Set<BusinessChartUserSetup>().Remove(entity);
        return Task.CompletedTask;
    }
}
