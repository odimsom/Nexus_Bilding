using nexus_bilding_api.core.application.DTOs.Stock;
using nexus_bilding_api.core.domain.Entities;

namespace nexus_bilding_api.core.application.Mappings;

public static class StockMapper
{
    public static StockTransactionDto ToDto(StockTransaction entity)
    {
        return new StockTransactionDto
        {
            Id = entity.Id.ToString(),
            ProductId = entity.ProductId.ToString(),
            Type = entity.Type,
            Quantity = entity.Quantity,
            Reference = entity.Reference,
            Notes = null, // Not in Entity
            TransactionDate = entity.CreatedAt
        };
    }

    public static StockTransaction ToEntity(CreateStockTransactionDto dto)
    {
        return new StockTransaction
        {
            ProductId = Guid.Parse(dto.ProductId),
            Type = dto.Type,
            Quantity = dto.Quantity,
            Reference = dto.Reference
            // Notes ignored
            // Date handled by BaseEntity.Created
        };
    }
}
