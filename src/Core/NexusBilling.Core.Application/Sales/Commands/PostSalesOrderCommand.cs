using MediatR;

namespace NexusBilling.Core.Application.Sales.Commands;

public record PostSalesOrderCommand(Guid TenantId, string No) : IRequest<PostSalesOrderResult>;

public record PostSalesOrderResult(string InvoiceNo, bool EcfProcessed = false, string? EcfTrackId = null);
