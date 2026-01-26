using nexus_bilding_api.core.application.DTOs.Product;
using nexus_bilding_api.core.domain.Entities;

namespace nexus_bilding_api.core.application.Mappings;

public static class ProductMapper
{
    public static ProductDto ToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id.ToString(),
            Name = product.Name,
            Description = product.Description,
            Sku = product.Sku,
            Category = product.Category,
            Price = product.Price,
            Cost = product.Cost,
            ItbisRate = product.ItbisRate,
            ExemptionReason = product.ExemptionReason,
            Stock = product.Stock,
            LowStockThreshold = product.LowStockThreshold,
            Status = product.Status,
            CreatedAt = product.CreatedAt.ToString("yyyy-MM-dd") ?? string.Empty
        };
    }

    public static Product ToEntity(CreateProductDto dto)
    {
        return new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Sku = dto.Sku,
            Category = dto.Category,
            Price = dto.Price,
            Cost = dto.Cost,
            ItbisRate = dto.ItbisRate,
            ExemptionReason = dto.ExemptionReason,
            Stock = dto.Stock,
            LowStockThreshold = dto.LowStockThreshold,
            Status = nexus_bilding_api.core.domain.Enums.ProductStatus.Active // Default to Active
        };
    }

    public static void UpdateEntity(Product product, CreateProductDto dto)
    {
        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Sku = dto.Sku;
        product.Category = dto.Category;
        product.Price = dto.Price;
        product.Cost = dto.Cost;
        product.ItbisRate = dto.ItbisRate;
        product.ExemptionReason = dto.ExemptionReason;
        product.Stock = dto.Stock;
        product.LowStockThreshold = dto.LowStockThreshold;
    }
}
