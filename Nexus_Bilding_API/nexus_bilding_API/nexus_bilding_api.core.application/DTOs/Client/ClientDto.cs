using nexus_bilding_api.core.domain.Enums;

namespace nexus_bilding_api.core.application.DTOs.Client;

public class ClientDto
{
    public string Id { get; set; } = string.Empty;
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
    public string CreatedAt { get; set; } = string.Empty;
}

public class CreateClientDto
{
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string TaxId { get; set; } = string.Empty;
    public bool IsCompany { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Notes { get; set; }
}
