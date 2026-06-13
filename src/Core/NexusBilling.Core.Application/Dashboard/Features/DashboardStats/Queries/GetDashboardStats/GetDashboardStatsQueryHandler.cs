using MediatR;
using NexusBilling.Core.Domain.Inventory.Repositories;
using NexusBilling.Core.Domain.Sales.Repositories;
using NexusBilling.Core.Domain.Purchasing.Repositories;

namespace NexusBilling.Core.Application.Dashboard.Features.DashboardStats.Queries.GetDashboardStats;

public sealed class GetDashboardStatsQueryHandler(
    ICustomerRepository customerRepo,
    IItemRepository itemRepo,
    ISalesHeaderRepository salesRepo,
    IVendorRepository vendorRepo,
    IPurchaseHeaderRepository purchaseRepo)
    : IRequestHandler<GetDashboardStatsQuery, DashboardStatsDto>
{
    public async Task<DashboardStatsDto> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId;

        var (_, customerCount) = await customerRepo.ListAsync(tenantId, null, null, 1, 1, cancellationToken);
        var (_, itemCount)     = await itemRepo.ListAsync(tenantId, null, null, 1, 1, cancellationToken);
        var vendorCount        = await vendorRepo.CountForTenantAsync(tenantId, cancellationToken: cancellationToken);
        
        var (allSalesOrders, _)     = await salesRepo.GetPagedForTenantAsync(tenantId, null, null, null, 1, 500, cancellationToken);
        var (allPurchaseOrders, _)  = await purchaseRepo.ListAsync(tenantId, null, 1, 500, cancellationToken);

        var now        = DateTime.UtcNow;
        var monthStart = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);

        var openOrders     = allSalesOrders.Count(o => o.Status == "Open" || o.Status == "Released");
        var totalThisMonth = allSalesOrders.Where(o => o.PostingDate >= monthStart).Sum(o => o.AmountIncludingVat);
        var totalAllTime   = allSalesOrders.Sum(o => o.AmountIncludingVat);

        var openPurchases     = allPurchaseOrders.Count(o => o.Status == "Open" || o.Status == "Released");
        var totalPurchasesThisMonth = allPurchaseOrders.Where(o => o.PostingDate >= monthStart).Sum(o => o.AmountIncludingVat);
        var totalPurchasesAllTime   = allPurchaseOrders.Sum(o => o.AmountIncludingVat);

        var recent = allSalesOrders
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

        // Last 12 months of sales grouped by month
        var twelveMonthsAgo = now.AddMonths(-11);
        var monthStart12    = new DateTime(twelveMonthsAgo.Year, twelveMonthsAgo.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        var monthlySales = allSalesOrders
            .Where(o => o.PostingDate >= monthStart12)
            .GroupBy(o => new { o.PostingDate.Year, o.PostingDate.Month })
            .Select(g => new MonthlyTotalDto(
                $"{g.Key.Year}-{g.Key.Month:D2}",
                g.Sum(o => o.AmountIncludingVat)))
            .OrderBy(m => m.Month)
            .ToList();

        return new DashboardStatsDto(customerCount, itemCount, openOrders, totalThisMonth, totalAllTime, vendorCount, openPurchases, totalPurchasesThisMonth, totalPurchasesAllTime, recent, monthlySales);
    }
}
