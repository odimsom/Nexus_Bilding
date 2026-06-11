using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class Insurance : Entity
{
    private Insurance() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public DateTime? EffectiveDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public string PolicyNo { get; private set; }
    public decimal AnnualPremium { get; private set; }
    public decimal PolicyCoverage { get; private set; }
    public string InsuranceType { get; private set; }
    public DateTime? LastDateModified { get; private set; }
    public string InsuranceVendorNo { get; private set; }
    public string FaClassCode { get; private set; }
    public string FaSubclassCode { get; private set; }
    public string FaLocationCode { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public string LocationCode { get; private set; }
    public bool Blocked { get; private set; }
    public string Description { get; private set; }
    public string SearchDescription { get; private set; }
    public string NoSeries { get; private set; }

    public static OperationResult<Insurance, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<Insurance, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new Insurance()
        {
            TenantId = tenantId
        };
        return OperationResult<Insurance, DomainError>.Ok(entity);
    }
}
