using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Administration.Entities;
using NexusBilling.Core.Domain.Administration.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Administration.Repositories;

public class ServiceHeaderRepository(NexusBillingDbContext dbContext) : GenericRepository<ServiceHeader>(dbContext), IServiceHeaderRepository
{
    public async Task<ServiceHeader?> GetByNoForTenantAsync(Guid tenantId, string no, CancellationToken ct = default)
        => await dbContext.ServiceHeaders
            .FirstOrDefaultAsync(h => h.TenantId.Value == tenantId && h.No == no, ct);

    public async Task<(IReadOnlyList<ServiceHeader> Items, int TotalCount)> GetPagedForTenantAsync(
        Guid tenantId, string? docType, string? status, string? search,
        int page, int pageSize, CancellationToken ct = default)
    {
        var q = dbContext.ServiceHeaders.Where(h => h.TenantId.Value == tenantId);
        if (!string.IsNullOrWhiteSpace(docType) && short.TryParse(docType, out var dt))
            q = q.Where(h => h.DocumentType == dt);
        if (!string.IsNullOrWhiteSpace(status) && short.TryParse(status, out var st))
            q = q.Where(h => h.Status == st);
        if (!string.IsNullOrWhiteSpace(search))
            q = q.Where(h => h.No.Contains(search) || h.CustomerNo.Contains(search) || h.Name.Contains(search) || h.Description.Contains(search));
        var total = await q.CountAsync(ct);
        var items = await q.OrderByDescending(h => h.OrderDate).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);
        return (items, total);
    }
}
