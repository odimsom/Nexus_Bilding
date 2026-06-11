using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IAccountingPeriodRepository
{
    Task<AccountingPeriod?> GetByIdAsync(long id);
    Task<IEnumerable<AccountingPeriod>> GetAllAsync();
    Task AddAsync(AccountingPeriod entity);
    Task UpdateAsync(AccountingPeriod entity);
    Task DeleteAsync(AccountingPeriod entity);
}
