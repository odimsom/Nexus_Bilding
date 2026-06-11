using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class IcOutboxTransaction : Entity
{
    private IcOutboxTransaction() { }

    public TenantIdentifier TenantId { get; private set; }
    public int TransactionNo { get; private set; }
    public string IcPartnerCode { get; private set; }
    public short SourceType { get; private set; }
    public short DocumentType { get; private set; }
    public string DocumentNo { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public short TransactionSource { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public short LineAction { get; private set; }
    public string IcPartnerGLAccNo { get; private set; }
    public int SourceLineNo { get; private set; }

    public static OperationResult<IcOutboxTransaction, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<IcOutboxTransaction, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new IcOutboxTransaction()
        {
            TenantId = tenantId
        };
        return OperationResult<IcOutboxTransaction, DomainError>.Ok(entity);
    }
}
