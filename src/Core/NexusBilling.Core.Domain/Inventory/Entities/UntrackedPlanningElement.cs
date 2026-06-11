using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class UntrackedPlanningElement : Entity
{
    private UntrackedPlanningElement() { }

    public TenantIdentifier TenantId { get; private set; }
    public string WorksheetTemplateName { get; private set; }
    public string WorksheetBatchName { get; private set; }
    public int WorksheetLineNo { get; private set; }
    public int TrackLineNo { get; private set; }
    public string ItemNo { get; private set; }
    public string VariantCode { get; private set; }
    public string LocationCode { get; private set; }
    public int SourceType { get; private set; }
    public string SourceId { get; private set; }
    public decimal ParameterValue { get; private set; }
    public decimal UntrackedQuantity { get; private set; }
    public decimal TrackQuantityFrom { get; private set; }
    public decimal TrackQuantityTo { get; private set; }
    public string Source { get; private set; }
    public short WarningLevel { get; private set; }

    public static OperationResult<UntrackedPlanningElement, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<UntrackedPlanningElement, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new UntrackedPlanningElement()
        {
            TenantId = tenantId
        };
        return OperationResult<UntrackedPlanningElement, DomainError>.Ok(entity);
    }
}
