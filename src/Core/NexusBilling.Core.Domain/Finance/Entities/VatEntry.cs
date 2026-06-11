using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class VatEntry : Entity
{
    private VatEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public string DocumentNo { get; private set; }
    public short DocumentType { get; private set; }
    public short Type { get; private set; }
    public decimal Base { get; private set; }
    public decimal Amount { get; private set; }
    public short VatCalculationType { get; private set; }
    public string BillToPayToNo { get; private set; }
    public bool Eu3PartyTrade { get; private set; }
    public string UserId { get; private set; }
    public string SourceCode { get; private set; }
    public string ReasonCode { get; private set; }
    public int ClosedByEntryNo { get; private set; }
    public bool Closed { get; private set; }
    public string CountryRegionCode { get; private set; }
    public string InternalRefNo { get; private set; }
    public int TransactionNo { get; private set; }
    public decimal UnrealizedAmount { get; private set; }
    public decimal UnrealizedBase { get; private set; }
    public decimal RemainingUnrealizedAmount { get; private set; }
    public decimal RemainingUnrealizedBase { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public string NoSeries { get; private set; }
    public string TaxAreaCode { get; private set; }
    public bool TaxLiable { get; private set; }
    public string TaxGroupCode { get; private set; }
    public bool UseTax { get; private set; }
    public string TaxJurisdictionCode { get; private set; }
    public string TaxGroupUsed { get; private set; }
    public short TaxType { get; private set; }
    public bool TaxOnTax { get; private set; }
    public int SalesTaxConnectionNo { get; private set; }
    public int UnrealizedVatEntryNo { get; private set; }
    public string VatBusPostingGroup { get; private set; }
    public string VatProdPostingGroup { get; private set; }
    public decimal AdditionalCurrencyAmount { get; private set; }
    public decimal AdditionalCurrencyBase { get; private set; }
    public decimal AddCurrencyUnrealizedAmt { get; private set; }
    public decimal AddCurrencyUnrealizedBase { get; private set; }
    public decimal VatBaseDiscount { get; private set; }
    public decimal AddCurrRemUnrealAmount { get; private set; }
    public decimal AddCurrRemUnrealBase { get; private set; }
    public decimal VatDifference { get; private set; }
    public decimal AddCurrVatDifference { get; private set; }
    public string ShipToOrderAddressCode { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public string VatRegistrationNo { get; private set; }
    public bool Reversed { get; private set; }
    public int ReversedByEntryNo { get; private set; }
    public int ReversedEntryNo { get; private set; }
    public bool EuService { get; private set; }

    public static OperationResult<VatEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<VatEntry, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new VatEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<VatEntry, DomainError>.Ok(entity);
    }
}
