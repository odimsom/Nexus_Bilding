using MediatR;

namespace NexusBilling.Core.Application.Purchasing.Features.PurchaseOrders.Commands.PostPurchaseOrder;

public record PostPurchaseOrderCommand(Guid TenantId, string No) : IRequest<PostPurchaseOrderResult>;

public record PostPurchaseOrderResult(string InvoiceNo);
