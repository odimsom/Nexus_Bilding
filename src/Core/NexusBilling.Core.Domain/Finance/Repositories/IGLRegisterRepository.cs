using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IGLRegisterRepository
{
    Task<GLRegister?> GetByIdAsync(long id);
    Task<IEnumerable<GLRegister>> GetAllAsync();
    Task AddAsync(GLRegister entity);
    Task UpdateAsync(GLRegister entity);
    Task DeleteAsync(GLRegister entity);
}
