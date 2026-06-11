using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IGenProductPostingGroupRepository
{
    Task<GenProductPostingGroup?> GetByIdAsync(long id);
    Task<IEnumerable<GenProductPostingGroup>> GetAllAsync();
    Task AddAsync(GenProductPostingGroup entity);
    Task UpdateAsync(GenProductPostingGroup entity);
    Task DeleteAsync(GenProductPostingGroup entity);
}
