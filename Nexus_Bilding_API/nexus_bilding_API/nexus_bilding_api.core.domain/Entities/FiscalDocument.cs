using nexus_bilding_api.core.domain.Common;
using nexus_bilding_api.core.domain.Enums;

namespace nexus_bilding_api.core.domain.Entities;

public class FiscalDocument : BaseEntity
{
    public FiscalDocumentType DocumentType { get; set; }
    public string? ECFNumber { get; set; }
    
    public Guid? ClientId { get; set; }
    public Client? Client { get; set; }
    
    public string ClientName { get; set; } = string.Empty;
    public string? ClientTaxId { get; set; }
    
    public DateTime IssueDate { get; set; }
    public DateTime? DueDate { get; set; }
    
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
    
    // Navigation Properties
    public ICollection<FiscalDocumentItem> Items { get; set; } = new List<FiscalDocumentItem>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
