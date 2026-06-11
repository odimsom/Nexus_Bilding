using NexusBilling.Core.Domain.Purchasing.Entities;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Domain.Purchasing.Repositories;

public interface IVendorRepository : IGenericRepository<Vendor>
{
    Task<Vendor?> GetByNoAsync(string no, CancellationToken cancellationToken = default);
}
