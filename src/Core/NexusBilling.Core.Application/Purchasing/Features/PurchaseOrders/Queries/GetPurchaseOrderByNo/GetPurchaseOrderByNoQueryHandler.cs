using MediatR;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Repositories;

namespace NexusBilling.Core.Application.Purchasing.Features.PurchaseOrders.Queries.GetPurchaseOrderByNo;

public sealed class GetPurchaseOrderByNoQueryHandler(
    IPurchaseHeaderRepository headerRepo,
    IPurchaseLineRepository lineRepo) : IRequestHandler<GetPurchaseOrderByNoQuery, PurchaseOrderDetailDto?>
{
    public async Task<PurchaseOrderDetailDto?> Handle(GetPurchaseOrderByNoQuery request, CancellationToken cancellationToken)
    {
        var tenantId = TenantIdentifier.Create(request.TenantId);
        var headers = await headerRepo.FindAsync(h => h.TenantId == tenantId && h.No == request.No && h.DocumentType == "Order");
        var header = headers.FirstOrDefault();

        if (header == null) return null;

        var lines = await lineRepo.FindAsync(l => l.TenantId == tenantId && l.DocumentNo == request.No && l.DocumentType == 1);
        
        var lineDtos = lines.OrderBy(l => l.LineNo).Select(l => new PurchaseOrderLineDetailDto(
            l.LineNo,
            l.No,
            l.Description,
            l.UnitOfMeasure,
            l.Quantity,
            l.DirectUnitCost,
            l.LineDiscount,
            l.LineDiscountAmount,
            l.Amount,
            l.AmountIncludingVat)).ToList();

        return new PurchaseOrderDetailDto(
            header.No,
            header.BuyFromVendorNo,
            header.PayToName,
            header.PostingDate,
            header.DueDate,
            header.Status,
            header.Amount,
            header.AmountIncludingVat,
            header.CurrencyCode,
            header.PaymentTermsCode,
            header.ExternalDocumentNo,
            lineDtos);
    }
}
