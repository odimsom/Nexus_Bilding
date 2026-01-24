using nexus_bilding_api.core.domain.Common;
using nexus_bilding_api.core.domain.Enums;

namespace nexus_bilding_api.core.domain.Entities;

public class Client : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    
    public string TaxId { get; set; } = string.Empty;
    public bool IsCompany { get; set; }
    
    public string? Address { get; set; }
    public string? City { get; set; }
    
    public ClientStatus Status { get; set; }
    
    public DateTime? VerifiedAt { get; set; }
    
    public string? Notes { get; set; }
    
    // Navigation Properties
    public ICollection<FiscalDocument> FiscalDocuments { get; set; } = new List<FiscalDocument>();
}
