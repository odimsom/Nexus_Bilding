using MediatR;

namespace NexusBilling.Core.Application.Sales.Features.SalesOrders.Commands.ReleaseSalesOrder;

public record ReleaseSalesOrderCommand(Guid TenantId, string No) : IRequest<bool>;
