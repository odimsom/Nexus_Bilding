using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Core.Domain.Sales.Repositories;

public interface ICustLedgerEntryRepository : IGenericRepository<CustLedgerEntry>
{
    Task<CustLedgerEntry?> GetByEntryNoAsync(int entryNo, CancellationToken cancellationToken = default);
}
