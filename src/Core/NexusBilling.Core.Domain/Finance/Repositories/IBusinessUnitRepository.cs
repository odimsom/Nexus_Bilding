using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IBusinessUnitRepository
{
    Task<BusinessUnit?> GetByIdAsync(long id);
    Task<IEnumerable<BusinessUnit>> GetAllAsync();
    Task AddAsync(BusinessUnit entity);
    Task UpdateAsync(BusinessUnit entity);
    Task DeleteAsync(BusinessUnit entity);
}
