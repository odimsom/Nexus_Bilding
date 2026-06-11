using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IGLEntryRepository
{
    Task<GLEntry?> GetByIdAsync(long id);
    Task<IEnumerable<GLEntry>> GetAllAsync();
    Task AddAsync(GLEntry entity);
    Task UpdateAsync(GLEntry entity);
    Task DeleteAsync(GLEntry entity);
}
