using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ConfigPackageTable : Entity
{
    private ConfigPackageTable() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PackageCode { get; private set; }
    public int TableId { get; private set; }
    public DateTime? ImportedDateAndTime { get; private set; }
    public DateTime? ExportedDateAndTime { get; private set; }
    public string Comments { get; private set; }
    public DateTime? CreatedDateAndTime { get; private set; }
    public string DataTemplate { get; private set; }
    public int PackageProcessingOrder { get; private set; }
    public int PageId { get; private set; }
    public int ProcessingOrder { get; private set; }
    public string ImportedByUserId { get; private set; }
    public string CreatedByUserId { get; private set; }
    public bool DimensionsAsColumns { get; private set; }
    public bool SkipTableTriggers { get; private set; }
    public bool DeleteRecsBeforeProcessing { get; private set; }
    public int ProcessingReportId { get; private set; }
    public int ParentTableId { get; private set; }
    public bool Validated { get; private set; }

    public static OperationResult<ConfigPackageTable, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ConfigPackageTable, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ConfigPackageTable()
        {
            TenantId = tenantId
        };
        return OperationResult<ConfigPackageTable, DomainError>.Ok(entity);
    }
}
