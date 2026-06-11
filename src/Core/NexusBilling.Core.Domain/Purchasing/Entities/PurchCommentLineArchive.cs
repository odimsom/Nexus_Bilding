using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class PurchCommentLineArchive : Entity
{
    private PurchCommentLineArchive() { }

    public TenantIdentifier TenantId { get; private set; }
    public short DocumentType { get; private set; }
    public string No { get; private set; }
    public int LineNo { get; private set; }
    public DateTime? Date { get; private set; }
    public string Code { get; private set; }
    public string Comment { get; private set; }
    public int DocumentLineNo { get; private set; }
    public int DocNoOccurrence { get; private set; }
    public int VersionNo { get; private set; }

    public static OperationResult<PurchCommentLineArchive, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PurchCommentLineArchive, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PurchCommentLineArchive()
        {
            TenantId = tenantId
        };
        return OperationResult<PurchCommentLineArchive, DomainError>.Ok(entity);
    }
}
