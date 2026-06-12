namespace NexusBilling.Core.Application.Inventory.DTOs;

public record ItemDto(
    string No, string Description, string Description2, string BaseUnitOfMeasure,
    decimal UnitPrice, decimal UnitCost, bool Blocked, decimal Inventory,
    string Type, string ItemCategoryCode, string InventoryPostingGroup,
    string GenProdPostingGroup, decimal StandardCost, decimal LastDirectCost);

public record ItemListDto(
    string No, string Description, string BaseUnitOfMeasure,
    decimal UnitPrice, decimal UnitCost, bool Blocked, decimal Inventory,
    string Type, string ItemCategoryCode);
