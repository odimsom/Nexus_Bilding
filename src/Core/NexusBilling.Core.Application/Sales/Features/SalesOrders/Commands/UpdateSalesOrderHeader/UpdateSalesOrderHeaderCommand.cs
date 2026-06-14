using MediatR;

namespace NexusBilling.Core.Application.Sales.Features.SalesOrders.Commands.UpdateSalesOrderHeader;

public record UpdateSalesOrderHeaderCommand(
    Guid TenantId,
    string No,
    DateTime? DueDate,
    string CurrencyCode,
    string PaymentTermsCode,
    string PaymentMethodCode,
    string SalespersonCode,
    string? ExternalDocumentNo) : IRequest<bool>;
