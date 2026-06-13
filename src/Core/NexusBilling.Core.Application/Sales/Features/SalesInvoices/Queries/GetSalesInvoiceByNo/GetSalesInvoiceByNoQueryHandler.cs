using MediatR;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Sales.Features.SalesInvoices.Queries.GetSalesInvoiceByNo;

public sealed class GetSalesInvoiceByNoQueryHandler(ISalesInvoiceHeaderRepository headerRepo, ISalesInvoiceLineRepository lineRepo)
    : IRequestHandler<GetSalesInvoiceByNoQuery, SalesInvoiceDetailDto?>
{
    public async Task<SalesInvoiceDetailDto?> Handle(GetSalesInvoiceByNoQuery request, CancellationToken cancellationToken)
    {
        var header = await headerRepo.GetByNoAsync(request.No, cancellationToken);
        if (header == null || header.TenantId.Value != request.TenantId)
            return null;

        var lines = await lineRepo.GetByDocumentNoAsync(request.No, cancellationToken);

        var lineDtos = lines.Select(l => new SalesInvoiceLineDto(
            l.Description,
            l.Quantity,
            l.UnitPrice,
            l.Amount,
            l.AmountIncludingVat,
            l.UnitOfMeasureCode,
            l.Vat)).ToList();

        return new SalesInvoiceDetailDto(
            header.No,
            header.SellToCustomerNo,
            header.BillToName,
            header.PostingDate ?? DateTime.MinValue,
            header.Amount,
            header.AmountIncludingVat,
            header.CurrencyCode,
            header.PaymentTermsCode,
            lineDtos);
    }
}
