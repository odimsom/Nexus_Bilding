using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ReminderTerms : Entity
{
    private ReminderTerms() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public bool PostInterest { get; private set; }
    public bool PostAdditionalFee { get; private set; }
    public int MaxNoOfReminders { get; private set; }
    public decimal MinimumAmountLcy { get; private set; }
    public bool PostAddFeePerLine { get; private set; }
    public string NoteAboutLineFeeOnReport { get; private set; }

    public static OperationResult<ReminderTerms, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ReminderTerms, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ReminderTerms()
        {
            TenantId = tenantId
        };
        return OperationResult<ReminderTerms, DomainError>.Ok(entity);
    }
}
