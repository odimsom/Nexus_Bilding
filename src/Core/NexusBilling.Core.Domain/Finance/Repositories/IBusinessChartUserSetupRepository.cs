using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IBusinessChartUserSetupRepository
{
    Task<BusinessChartUserSetup?> GetByIdAsync(long id);
    Task<IEnumerable<BusinessChartUserSetup>> GetAllAsync();
    Task AddAsync(BusinessChartUserSetup entity);
    Task UpdateAsync(BusinessChartUserSetup entity);
    Task DeleteAsync(BusinessChartUserSetup entity);
}
