using nexus_bilding_api.core.application.DTOs.Payment;
using nexus_bilding_api.core.domain.Entities;

namespace nexus_bilding_api.core.application.Mappings;

public static class PaymentMapper
{
    public static PaymentDto ToDto(Payment entity)
    {
        return new PaymentDto
        {
            Id = entity.Id.ToString(),
            FiscalDocumentId = entity.FiscalDocumentId.ToString(),
            Amount = entity.Amount,
            PaymentMethod = entity.Method,
            PaymentDate = entity.PaidAt,
            Reference = null, // Not in Entity
            Notes = null // Not in Entity
        };
    }

    public static Payment ToEntity(CreatePaymentDto dto)
    {
        return new Payment
        {
            FiscalDocumentId = Guid.Parse(dto.FiscalDocumentId),
            Amount = dto.Amount,
            Method = dto.PaymentMethod,
            PaidAt = dto.PaymentDate
        };
    }
}
