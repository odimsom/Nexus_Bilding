using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IGLAccountCategoryRepository
{
    Task<GLAccountCategory?> GetByIdAsync(long id);
    Task<IEnumerable<GLAccountCategory>> GetAllAsync();
    Task AddAsync(GLAccountCategory entity);
    Task UpdateAsync(GLAccountCategory entity);
    Task DeleteAsync(GLAccountCategory entity);
}
