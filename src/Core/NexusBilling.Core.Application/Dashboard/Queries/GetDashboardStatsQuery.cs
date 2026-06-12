using MediatR;

namespace NexusBilling.Core.Application.Dashboard.Queries;

public record MonthlyTotalDto(string Month, decimal Total);

public record DashboardStatsDto(
    int TotalCustomers,
    int TotalItems,
    int OpenOrders,
    decimal TotalSalesThisMonth,
    decimal TotalSalesAllTime,
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
