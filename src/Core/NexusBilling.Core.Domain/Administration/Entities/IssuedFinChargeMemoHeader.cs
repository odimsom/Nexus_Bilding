using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class IssuedFinChargeMemoHeader : Entity
{
    private IssuedFinChargeMemoHeader() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string CustomerNo { get; private set; }
    public string Name { get; private set; }
    public string Name2 { get; private set; }
    public string Address { get; private set; }
    public string Address2 { get; private set; }
    public string PostCode { get; private set; }
    public string City { get; private set; }
    public string County { get; private set; }
    public string CountryRegionCode { get; private set; }
    public string LanguageCode { get; private set; }
    public string CurrencyCode { get; private set; }
    public string Contact { get; private set; }
    public string YourReference { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public string CustomerPostingGroup { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string VatRegistrationNo { get; private set; }
    public string ReasonCode { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public DateTime? DueDate { get; private set; }
    public string FinChargeTermsCode { get; private set; }
    public bool InterestPosted { get; private set; }
    public bool AdditionalFeePosted { get; private set; }
    public string PostingDescription { get; private set; }
    public int NoPrinted { get; private set; }
    public string UserId { get; private set; }
    public string NoSeries { get; private set; }
    public string PreAssignedNoSeries { get; private set; }
    public string PreAssignedNo { get; private set; }
    public string SourceCode { get; private set; }
    public string TaxAreaCode { get; private set; }
    public bool TaxLiable { get; private set; }
    public string VatBusPostingGroup { get; private set; }
    public int DimensionSetId { get; private set; }

    public static OperationResult<IssuedFinChargeMemoHeader, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<IssuedFinChargeMemoHeader, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new IssuedFinChargeMemoHeader()
        {
            TenantId = tenantId
        };
        return OperationResult<IssuedFinChargeMemoHeader, DomainError>.Ok(entity);
    }
}
