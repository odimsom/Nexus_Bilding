using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class InteractionLogEntry : Entity
{
    private InteractionLogEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string ContactNo { get; private set; }
    public string ContactCompanyNo { get; private set; }
    public DateTime? Date { get; private set; }
    public string Description { get; private set; }
    public short InformationFlow { get; private set; }
    public short InitiatedBy { get; private set; }
    public int AttachmentNo { get; private set; }
    public decimal CostLcy { get; private set; }
    public decimal DurationMin { get; private set; }
    public string UserId { get; private set; }
    public string InteractionGroupCode { get; private set; }
    public string InteractionTemplateCode { get; private set; }
    public string CampaignNo { get; private set; }
    public int CampaignEntryNo { get; private set; }
    public bool CampaignResponse { get; private set; }
    public bool CampaignTarget { get; private set; }
    public string SegmentNo { get; private set; }
    public short Evaluation { get; private set; }
    public string TimeOfInteraction { get; private set; }
    public bool AttemptFailed { get; private set; }
    public string ToDoNo { get; private set; }
    public string SalespersonCode { get; private set; }
    public short DeliveryStatus { get; private set; }
    public bool Canceled { get; private set; }
    public short CorrespondenceType { get; private set; }
    public string ContactAltAddressCode { get; private set; }
    public int LoggedSegmentEntryNo { get; private set; }
    public short DocumentType { get; private set; }
    public string DocumentNo { get; private set; }
    public int VersionNo { get; private set; }
    public int DocNoOccurrence { get; private set; }
    public string ContactVia { get; private set; }
    public bool SendWordDocsAsAttmt { get; private set; }
    public string InteractionLanguageCode { get; private set; }
    public bool EMailLogged { get; private set; }
    public string Subject { get; private set; }
    public string OpportunityNo { get; private set; }
    public bool Postponed { get; private set; }

    public static OperationResult<InteractionLogEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<InteractionLogEntry, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new InteractionLogEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<InteractionLogEntry, DomainError>.Ok(entity);
    }
}
