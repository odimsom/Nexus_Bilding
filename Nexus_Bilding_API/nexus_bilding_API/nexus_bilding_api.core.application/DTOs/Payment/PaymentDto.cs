using nexus_bilding_api.core.domain.Enums;

namespace nexus_bilding_api.core.application.DTOs.Payment;

public class PaymentDto
{
    public string Id { get; set; } = string.Empty;
    public string FiscalDocumentId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? Reference { get; set; }
    public string? Notes { get; set; }
}

public class CreatePaymentDto
{
    public required string FiscalDocumentId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string? Reference { get; set; }
    public string? Notes { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
}
