using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IGeneralPostingSetupRepository
{
    Task<GeneralPostingSetup?> GetByIdAsync(long id);
    Task<IEnumerable<GeneralPostingSetup>> GetAllAsync();
    Task AddAsync(GeneralPostingSetup entity);
    Task UpdateAsync(GeneralPostingSetup entity);
    Task DeleteAsync(GeneralPostingSetup entity);
}
