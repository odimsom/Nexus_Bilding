using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class PaymentRegistrationSetup : Entity
{
    private PaymentRegistrationSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public string JournalTemplateName { get; private set; }
    public string JournalBatchName { get; private set; }
    public short BalAccountType { get; private set; }
    public string BalAccountNo { get; private set; }
    public bool UseThisAccountAsDef { get; private set; }
    public bool AutoFillDateReceived { get; private set; }

    public static OperationResult<PaymentRegistrationSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PaymentRegistrationSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PaymentRegistrationSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<PaymentRegistrationSetup, DomainError>.Ok(entity);
    }
}
