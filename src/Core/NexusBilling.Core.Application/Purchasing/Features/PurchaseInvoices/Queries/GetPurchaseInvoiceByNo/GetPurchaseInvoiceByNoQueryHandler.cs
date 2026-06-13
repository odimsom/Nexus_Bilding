using MediatR;
using NexusBilling.Core.Domain.Purchasing.Repositories;

namespace NexusBilling.Core.Application.Purchasing.Features.PurchaseInvoices.Queries.GetPurchaseInvoiceByNo;

public sealed class GetPurchaseInvoiceByNoQueryHandler(IPurchInvHeaderRepository headerRepo, IPurchInvLineRepository lineRepo)
    : IRequestHandler<GetPurchaseInvoiceByNoQuery, PurchaseInvoiceDetailDto?>
{
    public async Task<PurchaseInvoiceDetailDto?> Handle(GetPurchaseInvoiceByNoQuery request, CancellationToken cancellationToken)
    {
        var header = await headerRepo.GetByNoAsync(request.No, cancellationToken);
        if (header == null || header.TenantId.Value != request.TenantId)
            return null;

        var lines = await lineRepo.GetByDocumentNoAsync(request.No, cancellationToken);

        var lineDtos = lines.Select(l => new PurchaseInvoiceLineDto(
            l.Description,
            l.Quantity,
            l.DirectUnitCost,
            l.Amount,
            l.AmountIncludingVat,
            l.UnitOfMeasureCode,
            l.Vat)).ToList();

        return new PurchaseInvoiceDetailDto(
            header.No,
            header.BuyFromVendorNo,
            header.PayToName,
            header.PostingDate ?? DateTime.MinValue,
            lineDtos.Sum(l => l.Amount),
            lineDtos.Sum(l => l.AmountIncludingVat),
            header.CurrencyCode,
            header.PaymentTermsCode,
            lineDtos);
    }
}
