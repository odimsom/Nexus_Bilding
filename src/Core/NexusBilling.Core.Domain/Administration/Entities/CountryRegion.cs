using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class CountryRegion : Entity
{
    private CountryRegion() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string EuCountryRegionCode { get; private set; }
    public string IntrastatCode { get; private set; }
    public short AddressFormat { get; private set; }
    public short ContactAddressFormat { get; private set; }
    public string VatScheme { get; private set; }

    public static OperationResult<CountryRegion, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CountryRegion, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CountryRegion()
        {
            TenantId = tenantId
        };
        return OperationResult<CountryRegion, DomainError>.Ok(entity);
    }
}
