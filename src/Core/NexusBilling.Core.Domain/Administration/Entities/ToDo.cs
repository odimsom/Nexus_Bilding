using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ToDo : Entity
{
    private ToDo() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string TeamCode { get; private set; }
    public string SalespersonCode { get; private set; }
    public string CampaignNo { get; private set; }
    public string ContactNo { get; private set; }
    public string OpportunityNo { get; private set; }
    public string SegmentNo { get; private set; }
    public short Type { get; private set; }
    public DateTime? Date { get; private set; }
    public short Status { get; private set; }
    public short Priority { get; private set; }
    public string Description { get; private set; }
    public bool Closed { get; private set; }
    public DateTime? DateClosed { get; private set; }
    public string NoSeries { get; private set; }
    public bool Canceled { get; private set; }
    public string ContactCompanyNo { get; private set; }
    public bool Recurring { get; private set; }
    public string RecurringDateInterval { get; private set; }
    public short CalcDueDateFrom { get; private set; }
    public string StartTime { get; private set; }
    public int Duration { get; private set; }
    public int OpportunityEntryNo { get; private set; }
    public DateTime? LastDateModified { get; private set; }
    public string LastTimeModified { get; private set; }
    public bool AllDayEvent { get; private set; }
    public string Location { get; private set; }
    public string OrganizerToDoNo { get; private set; }
    public string InteractionTemplateCode { get; private set; }
    public string LanguageCode { get; private set; }
    public int AttachmentNo { get; private set; }
    public string Subject { get; private set; }
    public decimal UnitCostLcy { get; private set; }
    public decimal UnitDurationMin { get; private set; }
    public short SystemToDoType { get; private set; }
    public string CompletedBy { get; private set; }
    public DateTime? EndingDate { get; private set; }
    public string EndingTime { get; private set; }
    public short WizardStep { get; private set; }
    public bool TeamToDo { get; private set; }
    public bool SendOnFinish { get; private set; }
    public string SegmentDescription { get; private set; }
    public string TeamMeetingOrganizer { get; private set; }
    public string ActivityCode { get; private set; }
    public string WizardContactName { get; private set; }
    public string WizardCampaignDescription { get; private set; }
    public string WizardOpportunityDescription { get; private set; }

    public static OperationResult<ToDo, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ToDo, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ToDo()
        {
            TenantId = tenantId
        };
        return OperationResult<ToDo, DomainError>.Ok(entity);
    }
}
