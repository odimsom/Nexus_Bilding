using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceCommentLine : Entity
{
    private ServiceCommentLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; }
    public int TableLineNo { get; private set; }
    public int LineNo { get; private set; }
    public string Comment { get; private set; }
    public DateTime? Date { get; private set; }
    public short TableSubtype { get; private set; }
    public short TableName { get; private set; }

    public static OperationResult<ServiceCommentLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServiceCommentLine, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServiceCommentLine()
        {
            TenantId = tenantId
        };
        return OperationResult<ServiceCommentLine, DomainError>.Ok(entity);
    }
}
