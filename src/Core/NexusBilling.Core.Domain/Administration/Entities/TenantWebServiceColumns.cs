using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class TenantWebServiceColumns : Entity
{
    private TenantWebServiceColumns() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryId { get; private set; }
    public int DataItem { get; private set; }
    public int FieldNumber { get; private set; }
    public string FieldName { get; private set; }
    public string Tenantwebserviceid { get; private set; }
    public bool Include { get; private set; }

    public static OperationResult<TenantWebServiceColumns, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TenantWebServiceColumns, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TenantWebServiceColumns()
        {
            TenantId = tenantId
        };
        return OperationResult<TenantWebServiceColumns, DomainError>.Ok(entity);
    }
}
