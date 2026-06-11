using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IGenJournalTemplateRepository
{
    Task<GenJournalTemplate?> GetByIdAsync(long id);
    Task<IEnumerable<GenJournalTemplate>> GetAllAsync();
    Task AddAsync(GenJournalTemplate entity);
    Task UpdateAsync(GenJournalTemplate entity);
    Task DeleteAsync(GenJournalTemplate entity);
}
