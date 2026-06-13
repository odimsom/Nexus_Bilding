using MediatR;
using NexusBilling.Core.Domain.Administration.Repositories;

namespace NexusBilling.Core.Application.Administration.Features.ServiceOrders.Queries.GetServiceOrderByNo;

public sealed class GetServiceOrderByNoQueryHandler(
    IServiceHeaderRepository headerRepo,
    IServiceLineRepository lineRepo)
    : IRequestHandler<GetServiceOrderByNoQuery, ServiceOrderDetailDto?>
{
    private static readonly Dictionary<short, string> DocTypeLabels = new()
    {
        [0] = "Cotización", [1] = "Orden", [2] = "Factura"
    };

    private static readonly Dictionary<short, string> StatusLabels = new()
    {
        [0] = "Pendiente", [1] = "En Proceso", [2] = "Terminado", [3] = "En Espera"
    };

    private static readonly Dictionary<short, string> LineTypeLabels = new()
    {
        [0] = string.Empty, [1] = "Artículo", [2] = "Recurso", [3] = "Costo", [4] = "Cuenta CG"
    };

    public async Task<ServiceOrderDetailDto?> Handle(GetServiceOrderByNoQuery request, CancellationToken cancellationToken)
    {
        var header = await headerRepo.GetByNoForTenantAsync(request.TenantId, request.No, cancellationToken);
        if (header is null) return null;

        var rawLines = await lineRepo.GetByDocumentNoAsync(header.DocumentType, header.No, cancellationToken);
        var lines = rawLines.Select(l => new ServiceLineDto(
            l.LineNo,
            l.Type,
            LineTypeLabels.GetValueOrDefault(l.Type, string.Empty),
            l.No ?? string.Empty,
            l.Description ?? string.Empty,
            l.UnitOfMeasure ?? string.Empty,
            l.Quantity,
            l.UnitPrice,
            l.LineDiscount,
            l.LineDiscountAmount,
            l.Amount,
            l.AmountIncludingVat,
            l.Vat
        )).ToList();

        var totalAmount = lines.Sum(l => l.Amount);
        var totalAmountVat = lines.Sum(l => l.AmountIncludingVat);

        return new ServiceOrderDetailDto(
            header.No,
            header.DocumentType,
            DocTypeLabels.GetValueOrDefault(header.DocumentType, "Orden"),
            header.CustomerNo ?? string.Empty,
            header.Name ?? string.Empty,
            header.Description ?? string.Empty,
            header.OrderDate?.ToString("yyyy-MM-dd"),
            header.StartingDate?.ToString("yyyy-MM-dd"),
            header.FinishingDate?.ToString("yyyy-MM-dd"),
            header.DueDate?.ToString("yyyy-MM-dd"),
            header.PaymentTermsCode ?? string.Empty,
            header.PaymentMethodCode ?? string.Empty,
            header.SalespersonCode ?? string.Empty,
            header.CurrencyCode ?? string.Empty,
            header.ContractNo ?? string.Empty,
            header.Status,
            StatusLabels.GetValueOrDefault(header.Status, "Pendiente"),
            totalAmount,
            totalAmountVat,
            lines);
    }
}
