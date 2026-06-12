using MediatR;

namespace NexusBilling.Core.Application.Sales.Commands;

public record SetCustomerBlockedCommand(Guid TenantId, string No, bool Blocked) : IRequest<bool>;
