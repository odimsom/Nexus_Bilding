using MediatR;

namespace NexusBilling.Core.Application.Sales.Features.Quotations.Commands.CreateQuotation;

public record QuotationLineInput(
    string LineType,          // "Item" | "Service"
    string ItemNo,
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal LineDiscountPct,
    string UnitOfMeasure,
    decimal VatPct,
    // Service-specific
    short? ServiceBillingType,  // 0=TiempoEstandar 1=ValorUnico 2=TiempoEstimado
    DateTime? ServiceStartDate,
    DateTime? ServiceEndDate,
    decimal? ServiceHours,
    decimal? HourlyRate,
    string? ResourceNo);

public record CreateQuotationCommand(
    Guid TenantId,
    string SellToCustomerNo,
    string SellToCustomerName,
    DateTime PostingDate,
    DateTime? ValidUntilDate,
    string? QuotedBy,
    string? Observations,
    string CurrencyCode,
    string PaymentTermsCode,
    string PaymentMethodCode,
    string? ExternalDocumentNo,
    string? SeriesCode,
    IReadOnlyList<QuotationLineInput> Lines) : IRequest<CreateQuotationResult>;

public record CreateQuotationResult(string No);
