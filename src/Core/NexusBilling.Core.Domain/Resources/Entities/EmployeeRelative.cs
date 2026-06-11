using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class EmployeeRelative : Entity
{
    private EmployeeRelative() { }

    public TenantIdentifier TenantId { get; private set; }
    public string EmployeeNo { get; private set; }
    public int LineNo { get; private set; }
    public string RelativeCode { get; private set; }
    public string FirstName { get; private set; }
    public string MiddleName { get; private set; }
    public string LastName { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public string PhoneNo { get; private set; }
    public string RelativeSEmployeeNo { get; private set; }

    public static OperationResult<EmployeeRelative, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<EmployeeRelative, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new EmployeeRelative()
        {
            TenantId = tenantId
        };
        return OperationResult<EmployeeRelative, DomainError>.Ok(entity);
    }
}
