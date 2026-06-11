using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class SalesShipmentHeader : Entity
{
    private SalesShipmentHeader() { }

    public TenantIdentifier TenantId { get; private set; }
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
    public string CustomerPriceGroup { get; private set; }
    public bool PricesIncludingVat { get; private set; }
    public string InvoiceDiscCode { get; private set; }
    public string CustomerDiscGroup { get; private set; }
    public string LanguageCode { get; private set; }
    public string SalespersonCode { get; private set; }
    public string OrderNo { get; private set; }
    public int NoPrinted { get; private set; }
    public string OnHold { get; private set; }
    public short AppliesToDocType { get; private set; }
    public string AppliesToDocNo { get; private set; }
    public string BalAccountNo { get; private set; }
    public string VatRegistrationNo { get; private set; }
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
    public string OrderNoSeries { get; private set; }
    public string UserId { get; private set; }
    public string SourceCode { get; private set; }
    public string TaxAreaCode { get; private set; }
    public bool TaxLiable { get; private set; }
    public string VatBusPostingGroup { get; private set; }
    public decimal VatBaseDiscount { get; private set; }
    public string QuoteNo { get; private set; }
    public int DimensionSetId { get; private set; }
    public string CampaignNo { get; private set; }
    public string SellToContactNo { get; private set; }
    public string BillToContactNo { get; private set; }
    public string OpportunityNo { get; private set; }
    public string ResponsibilityCenter { get; private set; }
    public DateTime? RequestedDeliveryDate { get; private set; }
    public DateTime? PromisedDeliveryDate { get; private set; }
    public string ShippingTime { get; private set; }
    public string OutboundWhseHandlingTime { get; private set; }
    public string ShippingAgentServiceCode { get; private set; }
    public bool AllowLineDisc { get; private set; }

    public static OperationResult<SalesShipmentHeader, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SalesShipmentHeader, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SalesShipmentHeader()
        {
            TenantId = tenantId
        };
        return OperationResult<SalesShipmentHeader, DomainError>.Ok(entity);
    }
}
