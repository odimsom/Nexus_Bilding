using MediatR;

namespace NexusBilling.Core.Application.Sales.Features.Quotations.Commands.UpdateQuotationHeader;

public record UpdateQuotationHeaderCommand(
    Guid TenantId,
    string No,
    DateTime? ValidUntilDate,
    string? QuotedBy,
    string? Observations,
    string CurrencyCode,
    string PaymentTermsCode,
    string PaymentMethodCode,
    string? ExternalDocumentNo) : IRequest<bool>;
