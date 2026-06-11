using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class SalesHeaderArchive : Entity
{
    private SalesHeaderArchive() { }

    public TenantIdentifier TenantId { get; private set; }
    public short DocumentType { get; private set; }
    public string SellToCustomerNo { get; private set; }
    public string No { get; private set; }
    public string BillToCustomerNo { get; private set; }
    public string BillToName { get; private set; }
    public string BillToName2 { get; private set; }
    public string BillToAddress { get; private set; }
    public string BillToAddress2 { get; private set; }
    public string BillToCity { get; private set; }
    public string BillToContact { get; private set; }
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
    public DateTime? ShipmentDate { get; private set; }
    public string PostingDescription { get; private set; }
    public string PaymentTermsCode { get; private set; }
    public DateTime? DueDate { get; private set; }
    public decimal PaymentDiscount { get; private set; }
    public DateTime? PmtDiscountDate { get; private set; }
    public string ShipmentMethodCode { get; private set; }
    public string LocationCode { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public string CustomerPostingGroup { get; private set; }
    public string CurrencyCode { get; private set; }
    public decimal CurrencyFactor { get; private set; }
    public string PriceGroupCode { get; private set; }
    public bool PricesIncludingVat { get; private set; }
    public string InvoiceDiscCode { get; private set; }
    public string CustItemDiscGr { get; private set; }
    public string LanguageCode { get; private set; }
    public string SalespersonCode { get; private set; }
    public string OrderClass { get; private set; }
    public int NoPrinted { get; private set; }
    public string OnHold { get; private set; }
    public short AppliesToDocType { get; private set; }
    public string AppliesToDocNo { get; private set; }
    public string BalAccountNo { get; private set; }
    public bool Ship { get; private set; }
    public bool Invoice { get; private set; }
    public string ShippingNo { get; private set; }
    public string PostingNo { get; private set; }
    public string LastShippingNo { get; private set; }
    public string LastPostingNo { get; private set; }
    public string PrepaymentNo { get; private set; }
    public string LastPrepaymentNo { get; private set; }
    public string PrepmtCrMemoNo { get; private set; }
    public string LastPrepmtCrMemoNo { get; private set; }
    public string VatRegistrationNo { get; private set; }
    public bool CombineShipments { get; private set; }
    public string ReasonCode { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public bool Eu3PartyTrade { get; private set; }
    public string TransactionType { get; private set; }
    public string TransportMethod { get; private set; }
    public string VatCountryRegionCode { get; private set; }
    public string SellToCustomerName { get; private set; }
    public string SellToCustomerName2 { get; private set; }
    public string SellToAddress { get; private set; }
    public string SellToAddress2 { get; private set; }
    public string SellToCity { get; private set; }
    public string SellToContact { get; private set; }
    public string BillToPostCode { get; private set; }
    public string BillToCounty { get; private set; }
    public string BillToCountryRegionCode { get; private set; }
    public string SellToPostCode { get; private set; }
    public string SellToCounty { get; private set; }
    public string SellToCountryRegionCode { get; private set; }
    public string ShipToPostCode { get; private set; }
    public string ShipToCounty { get; private set; }
    public string ShipToCountryRegionCode { get; private set; }
    public short BalAccountType { get; private set; }
    public string ExitPoint { get; private set; }
    public bool Correction { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public string Area { get; private set; }
    public string TransactionSpecification { get; private set; }
    public string PaymentMethodCode { get; private set; }
    public string ShippingAgentCode { get; private set; }
    public string PackageTrackingNo { get; private set; }
    public string NoSeries { get; private set; }
    public string PostingNoSeries { get; private set; }
    public string ShippingNoSeries { get; private set; }
    public string TaxAreaCode { get; private set; }
    public bool TaxLiable { get; private set; }
    public string VatBusPostingGroup { get; private set; }
    public short Reserve { get; private set; }
    public string AppliesToId { get; private set; }
    public decimal VatBaseDiscount { get; private set; }
    public short Status { get; private set; }
    public short InvoiceDiscountCalculation { get; private set; }
    public decimal InvoiceDiscountValue { get; private set; }
    public bool SendIcDocument { get; private set; }
    public short IcStatus { get; private set; }
    public string SellToIcPartnerCode { get; private set; }
    public string BillToIcPartnerCode { get; private set; }
    public short IcDirection { get; private set; }
    public decimal Prepayment { get; private set; }
    public string PrepaymentNoSeries { get; private set; }
    public bool CompressPrepayment { get; private set; }
    public DateTime? PrepaymentDueDate { get; private set; }
    public string PrepmtCrMemoNoSeries { get; private set; }
    public string PrepmtPostingDescription { get; private set; }
    public DateTime? PrepmtPmtDiscountDate { get; private set; }
    public string PrepmtPaymentTermsCode { get; private set; }
    public decimal PrepmtPaymentDiscount { get; private set; }
    public string SalesQuoteNo { get; private set; }
    public byte[]? WorkDescription { get; private set; }
    public int DimensionSetId { get; private set; }
    public string CreditCardNo { get; private set; }
    public bool InteractionExist { get; private set; }
    public string TimeArchived { get; private set; }
    public DateTime? DateArchived { get; private set; }
    public string ArchivedBy { get; private set; }
    public int VersionNo { get; private set; }
    public int DocNoOccurrence { get; private set; }
    public string CampaignNo { get; private set; }
    public string SellToCustomerTemplateCode { get; private set; }
    public string SellToContactNo { get; private set; }
    public string BillToContactNo { get; private set; }
    public string BillToCustomerTemplateCode { get; private set; }
    public string OpportunityNo { get; private set; }
    public string ResponsibilityCenter { get; private set; }
    public short ShippingAdvice { get; private set; }
    public int PostingFromWhseRef { get; private set; }
    public DateTime? RequestedDeliveryDate { get; private set; }
    public DateTime? PromisedDeliveryDate { get; private set; }
    public string ShippingTime { get; private set; }
    public string OutboundWhseHandlingTime { get; private set; }
    public string ShippingAgentServiceCode { get; private set; }
    public bool Receive { get; private set; }
    public string ReturnReceiptNo { get; private set; }
    public string ReturnReceiptNoSeries { get; private set; }
    public string LastReturnReceiptNo { get; private set; }
    public bool AllowLineDisc { get; private set; }
    public bool GetShipmentUsed { get; private set; }
    public string AssignedUserId { get; private set; }

    public static OperationResult<SalesHeaderArchive, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SalesHeaderArchive, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SalesHeaderArchive()
        {
            TenantId = tenantId
        };
        return OperationResult<SalesHeaderArchive, DomainError>.Ok(entity);
    }
}
