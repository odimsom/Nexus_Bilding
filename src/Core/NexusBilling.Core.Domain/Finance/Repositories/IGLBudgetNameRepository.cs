using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IGLBudgetNameRepository
{
    Task<GLBudgetName?> GetByIdAsync(long id);
    Task<IEnumerable<GLBudgetName>> GetAllAsync();
    Task AddAsync(GLBudgetName entity);
    Task UpdateAsync(GLBudgetName entity);
    Task DeleteAsync(GLBudgetName entity);
}
