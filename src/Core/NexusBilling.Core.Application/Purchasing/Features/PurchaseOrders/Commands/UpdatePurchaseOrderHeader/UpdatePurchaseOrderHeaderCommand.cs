using MediatR;

namespace NexusBilling.Core.Application.Purchasing.Features.PurchaseOrders.Commands.UpdatePurchaseOrderHeader;

public record UpdatePurchaseOrderHeaderCommand(
    Guid TenantId,
    string No,
    DateTime? DueDate,
    string CurrencyCode,
    string PaymentTermsCode,
    string? ExternalDocumentNo) : IRequest<bool>;
