using MediatR;

namespace NexusBilling.Core.Application.Inventory.Features.Items.Commands.SetItemBlocked;

public record SetItemBlockedCommand(Guid TenantId, string No, bool Blocked) : IRequest<bool>;
