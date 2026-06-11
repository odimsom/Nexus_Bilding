using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IGeneralLedgerSetupRepository
{
    Task<GeneralLedgerSetup?> GetByIdAsync(long id);
    Task<IEnumerable<GeneralLedgerSetup>> GetAllAsync();
    Task AddAsync(GeneralLedgerSetup entity);
    Task UpdateAsync(GeneralLedgerSetup entity);
    Task DeleteAsync(GeneralLedgerSetup entity);
}
