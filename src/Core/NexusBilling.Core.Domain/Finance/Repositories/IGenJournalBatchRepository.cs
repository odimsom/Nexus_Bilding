using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IGenJournalBatchRepository
{
    Task<GenJournalBatch?> GetByIdAsync(long id);
    Task<IEnumerable<GenJournalBatch>> GetAllAsync();
    Task AddAsync(GenJournalBatch entity);
    Task UpdateAsync(GenJournalBatch entity);
    Task DeleteAsync(GenJournalBatch entity);
}
