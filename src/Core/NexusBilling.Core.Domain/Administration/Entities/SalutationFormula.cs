using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class SalutationFormula : Entity
{
    private SalutationFormula() { }

    public TenantIdentifier TenantId { get; private set; }
    public string SalutationCode { get; private set; }
    public string LanguageCode { get; private set; }
    public short SalutationType { get; private set; }
    public string Salutation { get; private set; }
    public short Name1 { get; private set; }
    public short Name2 { get; private set; }
    public short Name3 { get; private set; }
    public short Name4 { get; private set; }
    public short Name5 { get; private set; }

    public static OperationResult<SalutationFormula, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SalutationFormula, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SalutationFormula()
        {
            TenantId = tenantId
        };
        return OperationResult<SalutationFormula, DomainError>.Ok(entity);
    }
}
