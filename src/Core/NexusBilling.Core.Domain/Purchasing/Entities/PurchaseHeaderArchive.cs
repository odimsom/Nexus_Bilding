using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class PurchaseHeaderArchive : Entity
{
    private PurchaseHeaderArchive() { }

    public TenantIdentifier TenantId { get; private set; }
    public short DocumentType { get; private set; }
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
    public DateTime? OrderDate { get; private set; }
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
    public string OrderClass { get; private set; }
    public int NoPrinted { get; private set; }
    public string OnHold { get; private set; }
    public short AppliesToDocType { get; private set; }
    public string AppliesToDocNo { get; private set; }
    public string BalAccountNo { get; private set; }
    public bool Receive { get; private set; }
    public bool Invoice { get; private set; }
    public string ReceivingNo { get; private set; }
    public string PostingNo { get; private set; }
    public string LastReceivingNo { get; private set; }
    public string LastPostingNo { get; private set; }
    public string VendorOrderNo { get; private set; }
    public string VendorShipmentNo { get; private set; }
    public string VendorInvoiceNo { get; private set; }
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
    public string NoSeries { get; private set; }
    public string PostingNoSeries { get; private set; }
    public string ReceivingNoSeries { get; private set; }
    public string TaxAreaCode { get; private set; }
    public bool TaxLiable { get; private set; }
    public string VatBusPostingGroup { get; private set; }
    public string AppliesToId { get; private set; }
    public decimal VatBaseDiscount { get; private set; }
    public short Status { get; private set; }
    public short InvoiceDiscountCalculation { get; private set; }
    public decimal InvoiceDiscountValue { get; private set; }
    public bool SendIcDocument { get; private set; }
    public short IcStatus { get; private set; }
    public string BuyFromIcPartnerCode { get; private set; }
    public string PayToIcPartnerCode { get; private set; }
    public short IcDirection { get; private set; }
    public string PrepaymentNo { get; private set; }
    public string LastPrepaymentNo { get; private set; }
    public string PrepmtCrMemoNo { get; private set; }
    public string LastPrepmtCrMemoNo { get; private set; }
    public decimal Prepayment { get; private set; }
    public string PrepaymentNoSeries { get; private set; }
    public bool CompressPrepayment { get; private set; }
    public DateTime? PrepaymentDueDate { get; private set; }
    public string PrepmtCrMemoNoSeries { get; private set; }
    public string PrepmtPostingDescription { get; private set; }
    public DateTime? PrepmtPmtDiscountDate { get; private set; }
    public string PrepmtPaymentTermsCode { get; private set; }
    public decimal PrepmtPaymentDiscount { get; private set; }
    public string PurchaseQuoteNo { get; private set; }
    public int DimensionSetId { get; private set; }
    public bool InteractionExist { get; private set; }
    public string TimeArchived { get; private set; }
    public DateTime? DateArchived { get; private set; }
    public string ArchivedBy { get; private set; }
    public int VersionNo { get; private set; }
    public int DocNoOccurrence { get; private set; }
    public string CampaignNo { get; private set; }
    public string BuyFromContactNo { get; private set; }
    public string PayToContactNo { get; private set; }
    public string ResponsibilityCenter { get; private set; }
    public int PostingFromWhseRef { get; private set; }
    public DateTime? RequestedReceiptDate { get; private set; }
    public DateTime? PromisedReceiptDate { get; private set; }
    public string LeadTimeCalculation { get; private set; }
    public string InboundWhseHandlingTime { get; private set; }
    public string VendorAuthorizationNo { get; private set; }
    public string ReturnShipmentNo { get; private set; }
    public string ReturnShipmentNoSeries { get; private set; }
    public bool Ship { get; private set; }
    public string LastReturnShipmentNo { get; private set; }
    public string AssignedUserId { get; private set; }

    public static OperationResult<PurchaseHeaderArchive, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PurchaseHeaderArchive, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PurchaseHeaderArchive()
        {
            TenantId = tenantId
        };
        return OperationResult<PurchaseHeaderArchive, DomainError>.Ok(entity);
    }
}
