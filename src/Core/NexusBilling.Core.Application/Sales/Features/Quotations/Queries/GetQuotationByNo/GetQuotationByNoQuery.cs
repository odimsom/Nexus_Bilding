using MediatR;

namespace NexusBilling.Core.Application.Sales.Features.Quotations.Queries.GetQuotationByNo;

public record GetQuotationByNoQuery(Guid TenantId, string No) : IRequest<QuotationDetail?>;

public record QuotationDetailLine(
    int LineNo,
    string LineType,        // "Item" | "Service"
    string No,
    string Description,
    decimal Quantity,
    string UnitOfMeasure,
    decimal UnitPrice,
    decimal LineDiscountPct,
    decimal Amount,
    decimal AmountIncludingVat,
    decimal VatPct,
    // Service fields
    short? ServiceBillingType,
    string? ServiceStartDate,
    string? ServiceEndDate,
    decimal? ServiceHours,
    decimal? HourlyRate,
    string? ResourceNo);

public record QuotationDetail(
    string No,
    string SellToCustomerNo,
    string SellToCustomerName,
    string PostingDate,
    string? ValidUntilDate,
    string? QuotedBy,
    string? Observations,
    string Status,
    string CurrencyCode,
    string PaymentTermsCode,
    string PaymentMethodCode,
    string ExternalDocumentNo,
    decimal Amount,
    decimal AmountIncludingVat,
    bool IsExpired,
    IReadOnlyList<QuotationDetailLine> Lines);
