using MediatR;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Sales.Queries;

public sealed class GetSalesOrderByNoQueryHandler(
    ISalesHeaderRepository headerRepo,
    ISalesLineRepository lineRepo)
    : IRequestHandler<GetSalesOrderByNoQuery, SalesOrderDetailDto?>
{
    private static readonly Dictionary<string, short> DocTypeMap = new()
    {
        ["Order"] = 1, ["Quote"] = 0, ["Invoice"] = 2, ["Credit Memo"] = 3
    };

    public async Task<SalesOrderDetailDto?> Handle(GetSalesOrderByNoQuery request, CancellationToken cancellationToken)
    {
        var header = await headerRepo.GetByNoForTenantAsync(request.TenantId, request.No, cancellationToken);
        if (header is null) return null;

        DocTypeMap.TryGetValue(header.DocumentType, out var docTypeShort);
        var rawLines = await lineRepo.GetByDocumentNoAsync(docTypeShort, header.No, cancellationToken);
        var lines = rawLines.OrderBy(l => l.LineNo).Select(l => new SalesLineDto(
            l.LineNo,
            l.Type switch { 1 => "G/L Account", 2 => "Item", 3 => "Resource", 4 => "Fixed Asset", 5 => "Charge", _ => string.Empty },
            l.No, l.Description, l.UnitOfMeasure,
            l.Quantity, l.UnitPrice, l.LineDiscount, l.LineDiscountAmount,
            l.Amount, l.AmountIncludingVat, l.Vat)).ToList();

        return new SalesOrderDetailDto(
            header.No, header.DocumentType,
            header.SellToCustomerNo, header.SellToCustomerName,
            header.PostingDate.ToString("yyyy-MM-dd"),
            header.DueDate?.ToString("yyyy-MM-dd"),
            header.Amount, header.AmountIncludingVat,
            header.CurrencyCode, header.PaymentTermsCode,
            header.PaymentMethodCode, header.SalespersonCode,
            header.ExternalDocumentNo, header.Status, lines);
    }
}
