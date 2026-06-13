using MediatR;

namespace NexusBilling.Core.Application.Sales.Features.SalesOrders.Commands.PostSalesOrder;

public record PostSalesOrderCommand(Guid TenantId, string No) : IRequest<PostSalesOrderResult>;

public record PostSalesOrderResult(string InvoiceNo, bool EcfProcessed = false, string? EcfTrackId = null);
