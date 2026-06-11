using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class Opportunity : Entity
{
    private Opportunity() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string Description { get; private set; }
    public string SalespersonCode { get; private set; }
    public string CampaignNo { get; private set; }
    public string ContactNo { get; private set; }
    public string ContactCompanyNo { get; private set; }
    public string SalesCycleCode { get; private set; }
    public string SalesDocumentNo { get; private set; }
    public DateTime? CreationDate { get; private set; }
    public short Status { get; private set; }
    public short Priority { get; private set; }
    public bool Closed { get; private set; }
    public DateTime? DateClosed { get; private set; }
    public string NoSeries { get; private set; }
    public string SegmentNo { get; private set; }
    public short SalesDocumentType { get; private set; }
    public short WizardStep { get; private set; }
    public bool ActivateFirstStage { get; private set; }
    public string SegmentDescription { get; private set; }
    public decimal WizardEstimatedValueLcy { get; private set; }
    public decimal WizardChancesOfSuccess { get; private set; }
    public DateTime? WizardEstimatedClosingDate { get; private set; }
    public string WizardContactName { get; private set; }
    public string WizardCampaignDescription { get; private set; }

    public static OperationResult<Opportunity, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<Opportunity, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new Opportunity()
        {
            TenantId = tenantId
        };
        return OperationResult<Opportunity, DomainError>.Ok(entity);
    }
}
