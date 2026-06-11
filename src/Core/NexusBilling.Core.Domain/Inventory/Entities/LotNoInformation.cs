using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class LotNoInformation : Entity
{
    private LotNoInformation() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ItemNo { get; private set; }
    public string VariantCode { get; private set; }
    public string LotNo { get; private set; }
    public string Description { get; private set; }
    public short TestQuality { get; private set; }
    public string CertificateNumber { get; private set; }
    public bool Blocked { get; private set; }

    public static OperationResult<LotNoInformation, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<LotNoInformation, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new LotNoInformation()
        {
            TenantId = tenantId
        };
        return OperationResult<LotNoInformation, DomainError>.Ok(entity);
    }
}
