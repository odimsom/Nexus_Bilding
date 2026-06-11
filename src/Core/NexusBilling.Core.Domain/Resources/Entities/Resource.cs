using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class Resource : Entity
{
    private Resource() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public short Type { get; private set; }
    public string Name { get; private set; }
    public string SearchName { get; private set; }
    public string Name2 { get; private set; }
    public string Address { get; private set; }
    public string Address2 { get; private set; }
    public string City { get; private set; }
    public string SocialSecurityNo { get; private set; }
    public string JobTitle { get; private set; }
    public string Education { get; private set; }
    public string ContractClass { get; private set; }
    public DateTime? EmploymentDate { get; private set; }
    public string ResourceGroupNo { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public string BaseUnitOfMeasure { get; private set; }
    public decimal DirectUnitCost { get; private set; }
    public decimal IndirectCost { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal Profit { get; private set; }
    public short PriceProfitCalculation { get; private set; }
    public decimal UnitPrice { get; private set; }
    public string VendorNo { get; private set; }
    public DateTime? LastDateModified { get; private set; }
    public bool Blocked { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public byte[]? Picture { get; private set; }
    public string PostCode { get; private set; }
    public string County { get; private set; }
    public bool AutomaticExtTexts { get; private set; }
    public string NoSeries { get; private set; }
    public string TaxGroupCode { get; private set; }
    public string VatProdPostingGroup { get; private set; }
    public string CountryRegionCode { get; private set; }
    public string IcPartnerPurchGLAccNo { get; private set; }
    public Guid Image { get; private set; }
    public bool UseTimeSheet { get; private set; }
    public string TimeSheetOwnerUserId { get; private set; }
    public string TimeSheetApproverUserId { get; private set; }
    public string DefaultDeferralTemplateCode { get; private set; }
    public string ServiceZoneFilter { get; private set; }

    public static OperationResult<Resource, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<Resource, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new Resource()
        {
            TenantId = tenantId
        };
        return OperationResult<Resource, DomainError>.Ok(entity);
    }
}
