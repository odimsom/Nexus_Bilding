using MediatR;

namespace NexusBilling.Core.Application.Sales.Commands;

public record ReleaseSalesOrderCommand(Guid TenantId, string No) : IRequest<bool>;
