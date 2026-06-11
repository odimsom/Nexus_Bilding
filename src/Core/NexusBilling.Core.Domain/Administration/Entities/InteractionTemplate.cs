using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class InteractionTemplate : Entity
{
    private InteractionTemplate() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string InteractionGroupCode { get; private set; }
    public string Description { get; private set; }
    public decimal UnitCostLcy { get; private set; }
    public decimal UnitDurationMin { get; private set; }
    public short InformationFlow { get; private set; }
    public short InitiatedBy { get; private set; }
    public string CampaignNo { get; private set; }
    public bool CampaignTarget { get; private set; }
    public bool CampaignResponse { get; private set; }
    public short CorrespondenceTypeDefault { get; private set; }
    public string LanguageCodeDefault { get; private set; }
    public short WizardAction { get; private set; }
    public bool IgnoreContactCorresType { get; private set; }

    public static OperationResult<InteractionTemplate, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<InteractionTemplate, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new InteractionTemplate()
        {
            TenantId = tenantId
        };
        return OperationResult<InteractionTemplate, DomainError>.Ok(entity);
    }
}
