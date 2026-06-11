using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IRoundingMethodRepository
{
    Task<RoundingMethod?> GetByIdAsync(long id);
    Task<IEnumerable<RoundingMethod>> GetAllAsync();
    Task AddAsync(RoundingMethod entity);
    Task UpdateAsync(RoundingMethod entity);
    Task DeleteAsync(RoundingMethod entity);
}
