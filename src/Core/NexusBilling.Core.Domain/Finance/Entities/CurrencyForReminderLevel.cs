using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class CurrencyForReminderLevel : Entity
{
    private CurrencyForReminderLevel() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ReminderTermsCode { get; private set; }
    public int No { get; private set; }
    public string CurrencyCode { get; private set; }
    public decimal AdditionalFee { get; private set; }
    public decimal AddFeePerLine { get; private set; }

    public static OperationResult<CurrencyForReminderLevel, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CurrencyForReminderLevel, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CurrencyForReminderLevel()
        {
            TenantId = tenantId
        };
        return OperationResult<CurrencyForReminderLevel, DomainError>.Ok(entity);
    }
}
