using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class FinanceChargeMemoHeader : Entity
{
    private FinanceChargeMemoHeader() { }

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
    public bool PostInterest { get; private set; }
    public bool PostAdditionalFee { get; private set; }
    public string PostingDescription { get; private set; }
    public string NoSeries { get; private set; }
    public string IssuingNoSeries { get; private set; }
    public string IssuingNo { get; private set; }
    public string TaxAreaCode { get; private set; }
    public bool TaxLiable { get; private set; }
    public string VatBusPostingGroup { get; private set; }
    public int DimensionSetId { get; private set; }
    public string AssignedUserId { get; private set; }

    public static OperationResult<FinanceChargeMemoHeader, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FinanceChargeMemoHeader, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FinanceChargeMemoHeader()
        {
            TenantId = tenantId
        };
        return OperationResult<FinanceChargeMemoHeader, DomainError>.Ok(entity);
    }
}
