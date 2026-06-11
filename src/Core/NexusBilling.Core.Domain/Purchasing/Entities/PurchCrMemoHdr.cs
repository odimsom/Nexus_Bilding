using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class PurchCrMemoHdr : Entity
{
    private PurchCrMemoHdr() { }

    public TenantIdentifier TenantId { get; private set; }
    public string BuyFromVendorNo { get; private set; }
    public string No { get; private set; }
    public string PayToVendorNo { get; private set; }
    public string PayToName { get; private set; }
    public string PayToName2 { get; private set; }
    public string PayToAddress { get; private set; }
    public string PayToAddress2 { get; private set; }
    public string PayToCity { get; private set; }
    public string PayToContact { get; private set; }
    public string YourReference { get; private set; }
    public string ShipToCode { get; private set; }
    public string ShipToName { get; private set; }
    public string ShipToName2 { get; private set; }
    public string ShipToAddress { get; private set; }
    public string ShipToAddress2 { get; private set; }
    public string ShipToCity { get; private set; }
    public string ShipToContact { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public DateTime? ExpectedReceiptDate { get; private set; }
    public string PostingDescription { get; private set; }
    public string PaymentTermsCode { get; private set; }
    public DateTime? DueDate { get; private set; }
    public decimal PaymentDiscount { get; private set; }
    public DateTime? PmtDiscountDate { get; private set; }
    public string ShipmentMethodCode { get; private set; }
    public string LocationCode { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public string VendorPostingGroup { get; private set; }
    public string CurrencyCode { get; private set; }
    public decimal CurrencyFactor { get; private set; }
    public bool PricesIncludingVat { get; private set; }
    public string InvoiceDiscCode { get; private set; }
    public string LanguageCode { get; private set; }
    public string PurchaserCode { get; private set; }
    public int NoPrinted { get; private set; }
    public string OnHold { get; private set; }
    public short AppliesToDocType { get; private set; }
    public string AppliesToDocNo { get; private set; }
    public string BalAccountNo { get; private set; }
    public string VendorCrMemoNo { get; private set; }
    public string VatRegistrationNo { get; private set; }
    public string SellToCustomerNo { get; private set; }
    public string ReasonCode { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string TransactionType { get; private set; }
    public string TransportMethod { get; private set; }
    public string VatCountryRegionCode { get; private set; }
    public string BuyFromVendorName { get; private set; }
    public string BuyFromVendorName2 { get; private set; }
    public string BuyFromAddress { get; private set; }
    public string BuyFromAddress2 { get; private set; }
    public string BuyFromCity { get; private set; }
    public string BuyFromContact { get; private set; }
    public string PayToPostCode { get; private set; }
    public string PayToCounty { get; private set; }
    public string PayToCountryRegionCode { get; private set; }
    public string BuyFromPostCode { get; private set; }
    public string BuyFromCounty { get; private set; }
    public string BuyFromCountryRegionCode { get; private set; }
    public string ShipToPostCode { get; private set; }
    public string ShipToCounty { get; private set; }
    public string ShipToCountryRegionCode { get; private set; }
    public short BalAccountType { get; private set; }
    public string OrderAddressCode { get; private set; }
    public string EntryPoint { get; private set; }
    public bool Correction { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public string Area { get; private set; }
    public string TransactionSpecification { get; private set; }
    public string PaymentMethodCode { get; private set; }
    public string PreAssignedNoSeries { get; private set; }
    public string NoSeries { get; private set; }
    public string PreAssignedNo { get; private set; }
    public string UserId { get; private set; }
    public string SourceCode { get; private set; }
    public string TaxAreaCode { get; private set; }
    public bool TaxLiable { get; private set; }
    public string VatBusPostingGroup { get; private set; }
    public decimal VatBaseDiscount { get; private set; }
    public string PrepmtCrMemoNoSeries { get; private set; }
    public bool PrepaymentCreditMemo { get; private set; }
    public string PrepaymentOrderNo { get; private set; }
    public int DimensionSetId { get; private set; }
    public int VendorLedgerEntryNo { get; private set; }
    public string CampaignNo { get; private set; }
    public string BuyFromContactNo { get; private set; }
    public string PayToContactNo { get; private set; }
    public string ResponsibilityCenter { get; private set; }
    public string ReturnOrderNo { get; private set; }
    public string ReturnOrderNoSeries { get; private set; }

    public static OperationResult<PurchCrMemoHdr, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PurchCrMemoHdr, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PurchCrMemoHdr()
        {
            TenantId = tenantId
        };
        return OperationResult<PurchCrMemoHdr, DomainError>.Ok(entity);
    }
}
