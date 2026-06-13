using MediatR;

namespace NexusBilling.Core.Application.Sales.Features.Customers.Commands.SetCustomerBlocked;

public record SetCustomerBlockedCommand(Guid TenantId, string No, bool Blocked) : IRequest<bool>;
