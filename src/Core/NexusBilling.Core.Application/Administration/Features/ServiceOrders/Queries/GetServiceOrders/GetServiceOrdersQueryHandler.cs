using MediatR;
using NexusBilling.Core.Domain.Administration.Repositories;

namespace NexusBilling.Core.Application.Administration.Features.ServiceOrders.Queries.GetServiceOrders;

public sealed class GetServiceOrdersQueryHandler(IServiceHeaderRepository repo)
    : IRequestHandler<GetServiceOrdersQuery, (IReadOnlyList<ServiceOrderListItemDto> Items, int TotalCount)>
{
    private static readonly Dictionary<short, string> DocTypeLabels = new()
    {
        [0] = "Cotización", [1] = "Orden", [2] = "Factura"
    };

    private static readonly Dictionary<short, string> StatusLabels = new()
    {
        [0] = "Pendiente", [1] = "En Proceso", [2] = "Terminado", [3] = "Facturada"
    };

    public async Task<(IReadOnlyList<ServiceOrderListItemDto> Items, int TotalCount)> Handle(
        GetServiceOrdersQuery request, CancellationToken cancellationToken)
    {
        var (headers, total) = await repo.GetPagedForTenantAsync(
            request.TenantId, request.DocType, request.Status, request.Search,
            request.Page, request.PageSize, cancellationToken);

        var items = headers.Select(h => new ServiceOrderListItemDto(
            h.No,
            DocTypeLabels.GetValueOrDefault(h.DocumentType, "Orden"),
            h.CustomerNo ?? string.Empty,
            h.Name ?? string.Empty,
            h.Description ?? string.Empty,
            h.StartingDate?.ToString("yyyy-MM-dd"),
            h.FinishingDate?.ToString("yyyy-MM-dd"),
            h.OrderDate?.ToString("yyyy-MM-dd"),
            h.Status,
            StatusLabels.GetValueOrDefault(h.Status, "Pendiente"),
            h.ContractNo ?? string.Empty
        )).ToList();

        return (items, total);
    }
}
