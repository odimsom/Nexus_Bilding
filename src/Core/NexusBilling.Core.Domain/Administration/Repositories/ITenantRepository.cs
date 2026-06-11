using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Core.Domain.Administration.Repositories;

public interface ITenantRepository
{
    Task<Tenant?> GetByIdAsync(Guid id);
    Task<IEnumerable<Tenant>> GetAllAsync();
    Task AddAsync(Tenant tenant);
    void Update(Tenant tenant);
    void Delete(Tenant tenant);
}
