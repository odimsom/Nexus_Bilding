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
            l.LineNo,
            l.Type switch { 1 => "G/L Account", 3 => "Service", _ => "Item" },
            l.No ?? string.Empty,
            l.Description ?? string.Empty,
            l.Quantity,
            l.UnitPrice,
            l.LineDiscount,
            l.Amount,
            l.AmountIncludingVat,
            l.UnitOfMeasureCode ?? string.Empty,
            l.Vat)).ToList();

        return new SalesInvoiceDetailDto(
            header.No,
            header.SellToCustomerNo ?? string.Empty,
            header.SellToCustomerName ?? string.Empty,
            header.BillToName ?? string.Empty,
            header.PostingDate ?? DateTime.MinValue,
            header.DueDate,
            header.ExternalDocumentNo ?? string.Empty,
            header.CurrencyCode ?? string.Empty,
            header.PaymentTermsCode ?? string.Empty,
            header.PaymentMethodCode ?? string.Empty,
            header.SalespersonCode ?? string.Empty,
            header.OrderNo ?? string.Empty,
            header.Amount,
            header.AmountIncludingVat,
            lineDtos);
    }
}
