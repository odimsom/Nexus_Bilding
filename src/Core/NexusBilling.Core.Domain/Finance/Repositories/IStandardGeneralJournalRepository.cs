using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IStandardGeneralJournalRepository
{
    Task<StandardGeneralJournal?> GetByIdAsync(long id);
    Task<IEnumerable<StandardGeneralJournal>> GetAllAsync();
    Task AddAsync(StandardGeneralJournal entity);
    Task UpdateAsync(StandardGeneralJournal entity);
    Task DeleteAsync(StandardGeneralJournal entity);
}
