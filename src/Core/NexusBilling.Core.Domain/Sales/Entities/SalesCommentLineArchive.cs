using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class SalesCommentLineArchive : Entity
{
    private SalesCommentLineArchive() { }

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

    public static OperationResult<SalesCommentLineArchive, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SalesCommentLineArchive, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SalesCommentLineArchive()
        {
            TenantId = tenantId
        };
        return OperationResult<SalesCommentLineArchive, DomainError>.Ok(entity);
    }
}
