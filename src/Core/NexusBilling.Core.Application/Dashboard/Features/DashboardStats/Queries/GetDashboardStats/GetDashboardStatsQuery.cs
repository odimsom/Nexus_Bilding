using MediatR;

namespace NexusBilling.Core.Application.Dashboard.Features.DashboardStats.Queries.GetDashboardStats;

public record MonthlyTotalDto(string Month, decimal Total);

public record DashboardStatsDto(
    int TotalCustomers,
    int TotalItems,
    int OpenOrders,
    decimal TotalSalesThisMonth,
    decimal TotalSalesAllTime,
    int TotalVendors,
    int OpenPurchaseOrders,
    decimal TotalPurchasesThisMonth,
    decimal TotalPurchasesAllTime,
    IReadOnlyList<RecentOrderDto> RecentOrders,
    IReadOnlyList<MonthlyTotalDto> MonthlySales);

public record RecentOrderDto(
    string No,
    string DocumentType,
    string CustomerName,
    string PostingDate,
    string Status,
    decimal AmountIncludingVat);

public record GetDashboardStatsQuery(Guid TenantId) : IRequest<DashboardStatsDto>;
