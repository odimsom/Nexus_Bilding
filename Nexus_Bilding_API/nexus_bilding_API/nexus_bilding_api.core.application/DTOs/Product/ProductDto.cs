using nexus_bilding_api.core.domain.Enums;

namespace nexus_bilding_api.core.application.DTOs.Product;

public class ProductDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Sku { get; set; }
    public string? Category { get; set; }
    public decimal Price { get; set; }
    public decimal? Cost { get; set; }
    public ItbisRate ItbisRate { get; set; }
    public string? ExemptionReason { get; set; }
    public double? Stock { get; set; }
    public double? LowStockThreshold { get; set; }
    public ProductStatus Status { get; set; }
    public string CreatedAt { get; set; } = string.Empty;
}

public class CreateProductDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Sku { get; set; }
    public string? Category { get; set; }
    public decimal Price { get; set; }
    public decimal? Cost { get; set; }
    public ItbisRate ItbisRate { get; set; }
    public string? ExemptionReason { get; set; }
    public double? Stock { get; set; }
    public double? LowStockThreshold { get; set; }
}
