using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Core.Domain.Security.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User user);
    void Update(User user);
    void Delete(User user);
}
