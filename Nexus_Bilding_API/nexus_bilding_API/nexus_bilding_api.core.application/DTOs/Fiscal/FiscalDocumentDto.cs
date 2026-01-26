using nexus_bilding_api.core.domain.Enums;

namespace nexus_bilding_api.core.application.DTOs.Fiscal;

public class FiscalDocumentDto
{
    public string Id { get; set; } = string.Empty;
    public FiscalDocumentType DocumentType { get; set; }
    public string? ECFNumber { get; set; }

    public string? ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string? ClientTaxId { get; set; }

    public DateTime IssueDate { get; set; }
    public DateTime? DueDate { get; set; }

    public List<FiscalDocumentItemDto> Items { get; set; } = new();

    public decimal Subtotal { get; set; }
    public decimal TotalITBIS { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal TotalAmount { get; set; }

    public FiscalDocumentStatus Status { get; set; }

    public string? SecurityCode { get; set; }
    public string? XmlUrl { get; set; }
    public string? QrCodeContent { get; set; }
    public DateTime? SignedAt { get; set; }

    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class FiscalDocumentItemDto
{
    public string ProductId { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TaxRate { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
}

public class CreateFiscalDocumentDto
{
    public FiscalDocumentType DocumentType { get; set; }
    public string? ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string? ClientTaxId { get; set; }
    public string? Notes { get; set; }
    public DateTime IssueDate { get; set; } = DateTime.UtcNow;
    public DateTime? DueDate { get; set; }
    public List<CreateFiscalDocumentItemDto> Items { get; set; } = new();
}

public class CreateFiscalDocumentItemDto
{
    public string? ProductId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TaxRate { get; set; }
    public decimal Discount { get; set; }
}
