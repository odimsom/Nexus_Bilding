using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class IcPartner : Entity
{
    private IcPartner() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string CurrencyCode { get; private set; }
    public short InboxType { get; private set; }
    public string InboxDetails { get; private set; }
    public string ReceivablesAccount { get; private set; }
    public string PayablesAccount { get; private set; }
    public bool Blocked { get; private set; }
    public string CustomerNo { get; private set; }
    public string VendorNo { get; private set; }
    public short OutboundSalesItemNoType { get; private set; }
    public short OutboundPurchItemNoType { get; private set; }
    public bool CostDistributionInLcy { get; private set; }

    public static OperationResult<IcPartner, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<IcPartner, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new IcPartner()
        {
            TenantId = tenantId
        };
        return OperationResult<IcPartner, DomainError>.Ok(entity);
    }
}
