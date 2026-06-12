using MediatR;

namespace NexusBilling.Core.Application.Dashboard.Queries;

public record DashboardStatsDto(
    int TotalCustomers,
    int TotalItems,
    int OpenOrders,
    decimal TotalSalesThisMonth,
    decimal TotalSalesAllTime,
    IReadOnlyList<RecentOrderDto> RecentOrders);

public record RecentOrderDto(
    string No,
    string DocumentType,
    string CustomerName,
    string PostingDate,
    string Status,
    decimal AmountIncludingVat);

public record GetDashboardStatsQuery(Guid TenantId) : IRequest<DashboardStatsDto>;
