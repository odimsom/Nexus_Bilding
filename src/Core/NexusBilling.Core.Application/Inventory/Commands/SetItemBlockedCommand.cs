using MediatR;

namespace NexusBilling.Core.Application.Inventory.Commands;

public record SetItemBlockedCommand(Guid TenantId, string No, bool Blocked) : IRequest<bool>;
