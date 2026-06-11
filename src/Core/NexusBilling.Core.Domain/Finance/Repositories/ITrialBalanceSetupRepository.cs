using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface ITrialBalanceSetupRepository
{
    Task<TrialBalanceSetup?> GetByIdAsync(long id);
    Task<IEnumerable<TrialBalanceSetup>> GetAllAsync();
    Task AddAsync(TrialBalanceSetup entity);
    Task UpdateAsync(TrialBalanceSetup entity);
    Task DeleteAsync(TrialBalanceSetup entity);
}
