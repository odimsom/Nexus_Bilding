using MediatR;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Sales.Features.Quotations.Queries.GetQuotationByNo;

public sealed class GetQuotationByNoQueryHandler(
    ISalesHeaderRepository headerRepo,
    ISalesLineRepository lineRepo)
    : IRequestHandler<GetQuotationByNoQuery, QuotationDetail?>
{
    public async Task<QuotationDetail?> Handle(GetQuotationByNoQuery q, CancellationToken ct)
    {
        var header = await headerRepo.GetByNoForTenantAsync(q.TenantId, q.No, ct);
        if (header is null || header.DocumentType != "Quote") return null;

        var lines = await lineRepo.GetByDocumentNoAsync(0 /* Quote */, header.No, ct);

        var now = DateTime.UtcNow;
        var lineList = lines.OrderBy(l => l.LineNo).Select(l => new QuotationDetailLine(
            l.LineNo,
            l.Type == 3 ? "Service" : "Item",
            l.No ?? string.Empty,
            l.Description ?? string.Empty,
            l.Quantity,
            l.UnitOfMeasure ?? string.Empty,
            l.UnitPrice,
            l.LineDiscount,
            l.Amount,
            l.AmountIncludingVat,
            l.Vat,
            l.ServiceBillingType,
            l.ServiceStartDate?.ToString("yyyy-MM-dd"),
            l.ServiceEndDate?.ToString("yyyy-MM-dd"),
            l.ServiceHours,
            l.HourlyRate,
            l.ResourceNo
        )).ToList();

        return new QuotationDetail(
            header.No,
            header.SellToCustomerNo,
            header.SellToCustomerName,
            header.PostingDate.ToString("yyyy-MM-dd"),
            header.ValidUntilDate?.ToString("yyyy-MM-dd"),
            header.QuotedBy,
            header.Observations,
            header.Status,
            header.CurrencyCode,
            header.PaymentTermsCode,
            header.PaymentMethodCode,
            header.ExternalDocumentNo,
            header.Amount,
            header.AmountIncludingVat,
            header.ValidUntilDate.HasValue && header.ValidUntilDate.Value < now,
            lineList);
    }
}
