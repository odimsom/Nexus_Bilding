using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IStandardGeneralJournalLineRepository
{
    Task<StandardGeneralJournalLine?> GetByIdAsync(long id);
    Task<IEnumerable<StandardGeneralJournalLine>> GetAllAsync();
    Task AddAsync(StandardGeneralJournalLine entity);
    Task UpdateAsync(StandardGeneralJournalLine entity);
    Task DeleteAsync(StandardGeneralJournalLine entity);
}
