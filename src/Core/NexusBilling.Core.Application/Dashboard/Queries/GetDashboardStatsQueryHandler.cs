using MediatR;
using NexusBilling.Core.Domain.Inventory.Repositories;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Dashboard.Queries;

public sealed class GetDashboardStatsQueryHandler(
    ICustomerRepository customerRepo,
    IItemRepository itemRepo,
    ISalesHeaderRepository salesRepo)
    : IRequestHandler<GetDashboardStatsQuery, DashboardStatsDto>
{
    public async Task<DashboardStatsDto> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId;

        var (_, customerCount) = await customerRepo.ListAsync(tenantId, null, null, 1, 1, cancellationToken);
        var (_, itemCount)     = await itemRepo.ListAsync(tenantId, null, null, 1, 1, cancellationToken);
        var (allOrders, _)     = await salesRepo.GetPagedForTenantAsync(tenantId, null, null, null, 1, 500, cancellationToken);

        var now        = DateTime.UtcNow;
        var monthStart = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);

        var openOrders     = allOrders.Count(o => o.Status == "Open" || o.Status == "Released");
        var totalThisMonth = allOrders.Where(o => o.PostingDate >= monthStart).Sum(o => o.AmountIncludingVat);
        var totalAllTime   = allOrders.Sum(o => o.AmountIncludingVat);

        var recent = allOrders
            .OrderByDescending(o => o.PostingDate)
            .Take(8)
            .Select(o => new RecentOrderDto(
                o.No,
                o.DocumentType,
                o.SellToCustomerName,
                o.PostingDate.ToString("yyyy-MM-dd"),
                o.Status,
                o.AmountIncludingVat))
            .ToList();

        return new DashboardStatsDto(customerCount, itemCount, openOrders, totalThisMonth, totalAllTime, recent);
    }
}
