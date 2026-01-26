using nexus_bilding_api.core.domain.Enums;

namespace nexus_bilding_api.core.application.DTOs.Stock;

public class StockTransactionDto
{
    public string Id { get; set; } = string.Empty;
    public string ProductId { get; set; } = string.Empty;
    public StockTransactionType Type { get; set; }
    public double Quantity { get; set; }
    public string? Reference { get; set; }
    public string? Notes { get; set; }
    public DateTime TransactionDate { get; set; }
}

public class CreateStockTransactionDto
{
    public required string ProductId { get; set; }
    public StockTransactionType Type { get; set; }
    public double Quantity { get; set; }
    public string? Reference { get; set; }
    public string? Notes { get; set; }
}
