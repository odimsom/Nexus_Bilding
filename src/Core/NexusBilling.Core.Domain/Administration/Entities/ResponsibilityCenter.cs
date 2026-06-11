using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ResponsibilityCenter : Entity
{
    private ResponsibilityCenter() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string Address2 { get; private set; }
    public string City { get; private set; }
    public string PostCode { get; private set; }
    public string CountryRegionCode { get; private set; }
    public string PhoneNo { get; private set; }
    public string FaxNo { get; private set; }
    public string Name2 { get; private set; }
    public string Contact { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public string LocationCode { get; private set; }
    public string County { get; private set; }
    public string EMail { get; private set; }
    public string HomePage { get; private set; }

    public static OperationResult<ResponsibilityCenter, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ResponsibilityCenter, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ResponsibilityCenter()
        {
            TenantId = tenantId
        };
        return OperationResult<ResponsibilityCenter, DomainError>.Ok(entity);
    }
}
