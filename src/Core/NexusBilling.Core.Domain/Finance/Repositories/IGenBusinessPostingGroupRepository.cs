using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IGenBusinessPostingGroupRepository
{
    Task<GenBusinessPostingGroup?> GetByIdAsync(long id);
    Task<IEnumerable<GenBusinessPostingGroup>> GetAllAsync();
    Task AddAsync(GenBusinessPostingGroup entity);
    Task UpdateAsync(GenBusinessPostingGroup entity);
    Task DeleteAsync(GenBusinessPostingGroup entity);
}
