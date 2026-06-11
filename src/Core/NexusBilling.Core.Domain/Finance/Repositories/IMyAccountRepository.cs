using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IMyAccountRepository
{
    Task<MyAccount?> GetByIdAsync(long id);
    Task<IEnumerable<MyAccount>> GetAllAsync();
    Task AddAsync(MyAccount entity);
    Task UpdateAsync(MyAccount entity);
    Task DeleteAsync(MyAccount entity);
}
