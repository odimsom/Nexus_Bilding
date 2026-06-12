using MediatR;

namespace NexusBilling.Core.Application.Inventory.Commands;

public record UpsertItemCommand(
    Guid TenantId,
    string? ExistingNo,
    string No,
    string Description,
    string Description2,
    string BaseUnitOfMeasure,
    decimal UnitPrice,
    decimal UnitCost,
    decimal StandardCost,
    string Type,
    string ItemCategoryCode,
    string InventoryPostingGroup,
    string GenProdPostingGroup,
    string VatProdPostingGroup,
    string VendorNo,
    string VendorItemNo
) : IRequest<UpsertItemResult>;

public record UpsertItemResult(bool Created, string No);
