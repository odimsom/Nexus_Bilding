using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class PaymentTermTranslation : Entity
{
    private PaymentTermTranslation() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PaymentTerm { get; private set; }
    public string LanguageCode { get; private set; }
    public string Description { get; private set; }

    public static OperationResult<PaymentTermTranslation, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PaymentTermTranslation, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PaymentTermTranslation()
        {
            TenantId = tenantId
        };
        return OperationResult<PaymentTermTranslation, DomainError>.Ok(entity);
    }
}
