using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Core.Domain.Administration.Repositories;

public interface IServiceLineRepository : IGenericRepository<ServiceLine>
{
    Task<IReadOnlyList<ServiceLine>> GetByDocumentNoAsync(short docType, string documentNo, CancellationToken ct = default);
}
