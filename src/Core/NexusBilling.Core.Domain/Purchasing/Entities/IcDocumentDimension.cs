using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class IcDocumentDimension : Entity
{
    private IcDocumentDimension() { }

    public TenantIdentifier TenantId { get; private set; }
    public int TableId { get; private set; }
    public int TransactionNo { get; private set; }
    public string IcPartnerCode { get; private set; }
    public short TransactionSource { get; private set; }
    public int LineNo { get; private set; }
    public string DimensionCode { get; private set; }
    public string DimensionValueCode { get; private set; }

    public static OperationResult<IcDocumentDimension, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<IcDocumentDimension, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new IcDocumentDimension()
        {
            TenantId = tenantId
        };
        return OperationResult<IcDocumentDimension, DomainError>.Ok(entity);
    }
}
