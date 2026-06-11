using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IGenJournalLineRepository
{
    Task<GenJournalLine?> GetByIdAsync(long id);
    Task<IEnumerable<GenJournalLine>> GetAllAsync();
    Task AddAsync(GenJournalLine entity);
    Task UpdateAsync(GenJournalLine entity);
    Task DeleteAsync(GenJournalLine entity);
}
