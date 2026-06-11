using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ObjectOptions : Entity
{
    private ObjectOptions() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ParameterName { get; private set; }
    public int ObjectId { get; private set; }
    public short ObjectType { get; private set; }
    public string CompanyName { get; private set; }
    public string UserName { get; private set; }
    public byte[]? OptionData { get; private set; }
    public bool PublicVisible { get; private set; }
    public bool Temporary { get; private set; }
    public string CreatedBy { get; private set; }

    public static OperationResult<ObjectOptions, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ObjectOptions, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ObjectOptions()
        {
            TenantId = tenantId
        };
        return OperationResult<ObjectOptions, DomainError>.Ok(entity);
    }
}
