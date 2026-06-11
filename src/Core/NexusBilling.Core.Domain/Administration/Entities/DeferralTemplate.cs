using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class DeferralTemplate : Entity
{
    private DeferralTemplate() { }

    public TenantIdentifier TenantId { get; private set; }
    public string DeferralCode { get; private set; }
    public string Description { get; private set; }
    public string DeferralAccount { get; private set; }
    public decimal Deferral { get; private set; }
    public short CalcMethod { get; private set; }
    public short StartDate { get; private set; }
    public int NoOfPeriods { get; private set; }
    public string PeriodDescription { get; private set; }

    public static OperationResult<DeferralTemplate, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<DeferralTemplate, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new DeferralTemplate()
        {
            TenantId = tenantId
        };
        return OperationResult<DeferralTemplate, DomainError>.Ok(entity);
    }
}
