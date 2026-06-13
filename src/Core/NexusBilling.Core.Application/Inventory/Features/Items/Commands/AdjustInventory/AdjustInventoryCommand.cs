using MediatR;

namespace NexusBilling.Core.Application.Inventory.Features.Items.Commands.AdjustInventory;

public record AdjustInventoryCommand(
    Guid TenantId,
    string ItemNo,
    decimal Quantity,
    string DocumentNo,
    string Description,
    string UnitOfMeasureCode) : IRequest<int>;
