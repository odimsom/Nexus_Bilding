using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AdditionalFeeSetup : Entity
{
    private AdditionalFeeSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public bool ChargePerLine { get; private set; }
    public string ReminderTermsCode { get; private set; }
    public int ReminderLevelNo { get; private set; }
    public string CurrencyCode { get; private set; }
    public decimal ThresholdRemainingAmount { get; private set; }
    public decimal AdditionalFeeAmount { get; private set; }
    public decimal AdditionalFee { get; private set; }
    public decimal MinAdditionalFeeAmount { get; private set; }
    public decimal MaxAdditionalFeeAmount { get; private set; }

    public static OperationResult<AdditionalFeeSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AdditionalFeeSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AdditionalFeeSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<AdditionalFeeSetup, DomainError>.Ok(entity);
    }
}
