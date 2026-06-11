using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class FixedAsset : Entity
{
    private FixedAsset() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string Description { get; private set; }
    public string SearchDescription { get; private set; }
    public string Description2 { get; private set; }
    public string FaClassCode { get; private set; }
    public string FaSubclassCode { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public string LocationCode { get; private set; }
    public string FaLocationCode { get; private set; }
    public string VendorNo { get; private set; }
    public short MainAssetComponent { get; private set; }
    public string ComponentOfMainAsset { get; private set; }
    public bool BudgetedAsset { get; private set; }
    public DateTime? WarrantyDate { get; private set; }
    public string ResponsibleEmployee { get; private set; }
    public string SerialNo { get; private set; }
    public DateTime? LastDateModified { get; private set; }
    public bool Blocked { get; private set; }
    public byte[]? Picture { get; private set; }
    public string MaintenanceVendorNo { get; private set; }
    public bool UnderMaintenance { get; private set; }
    public DateTime? NextServiceDate { get; private set; }
    public bool Inactive { get; private set; }
    public string NoSeries { get; private set; }
    public string FaPostingGroup { get; private set; }
    public Guid Image { get; private set; }

    public static OperationResult<FixedAsset, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FixedAsset, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FixedAsset()
        {
            TenantId = tenantId
        };
        return OperationResult<FixedAsset, DomainError>.Ok(entity);
    }
}
