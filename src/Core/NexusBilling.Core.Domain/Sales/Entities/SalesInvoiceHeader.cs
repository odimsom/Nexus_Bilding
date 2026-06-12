using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class SalesInvoiceHeader : Entity
{
    private SalesInvoiceHeader() { }

    public TenantIdentifier TenantId { get; set; }
    public string SellToCustomerNo { get; set; }
    public string No { get; set; }
    public string BillToCustomerNo { get; set; }
    public string BillToName { get; set; }
    public string BillToName2 { get; set; }
    public string BillToAddress { get; set; }
    public string BillToAddress2 { get; set; }
    public string BillToCity { get; set; }
    public string BillToContact { get; set; }
    public string YourReference { get; set; }
    public string ShipToCode { get; set; }
    public string ShipToName { get; set; }
    public string ShipToName2 { get; set; }
    public string ShipToAddress { get; set; }
    public string ShipToAddress2 { get; set; }
    public string ShipToCity { get; set; }
    public string ShipToContact { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? PostingDate { get; set; }
    public DateTime? ShipmentDate { get; set; }
    public string PostingDescription { get; set; }
    public string PaymentTermsCode { get; set; }
    public DateTime? DueDate { get; set; }
    public decimal Amount { get; set; }
    public decimal AmountIncludingVat { get; set; }
    public decimal PaymentDiscount { get; set; }
    public DateTime? PmtDiscountDate { get; set; }
    public string ShipmentMethodCode { get; set; }
    public string LocationCode { get; set; }
    public string ShortcutDimension1Code { get; set; }
    public string ShortcutDimension2Code { get; set; }
    public string CustomerPostingGroup { get; set; }
    public string CurrencyCode { get; set; }
    public decimal CurrencyFactor { get; set; }
    public string CustomerPriceGroup { get; set; }
    public bool PricesIncludingVat { get; set; }
    public string InvoiceDiscCode { get; set; }
    public string CustomerDiscGroup { get; set; }
    public string LanguageCode { get; set; }
    public string SalespersonCode { get; set; }
    public string OrderNo { get; set; }
    public int NoPrinted { get; set; }
    public string OnHold { get; set; }
    public short AppliesToDocType { get; set; }
    public string AppliesToDocNo { get; set; }
    public string BalAccountNo { get; set; }
    public string VatRegistrationNo { get; set; }
    public string ReasonCode { get; set; }
    public string GenBusPostingGroup { get; set; }
    public bool Eu3PartyTrade { get; set; }
    public string TransactionType { get; set; }
    public string TransportMethod { get; set; }
    public string VatCountryRegionCode { get; set; }
    public string SellToCustomerName { get; set; }
    public string SellToCustomerName2 { get; set; }
    public string SellToAddress { get; set; }
    public string SellToAddress2 { get; set; }
    public string SellToCity { get; set; }
    public string SellToContact { get; set; }
    public string BillToPostCode { get; set; }
    public string BillToCounty { get; set; }
    public string BillToCountryRegionCode { get; set; }
    public string SellToPostCode { get; set; }
    public string SellToCounty { get; set; }
    public string SellToCountryRegionCode { get; set; }
    public string ShipToPostCode { get; set; }
    public string ShipToCounty { get; set; }
    public string ShipToCountryRegionCode { get; set; }
    public short BalAccountType { get; set; }
    public string ExitPoint { get; set; }
    public bool Correction { get; set; }
    public DateTime? DocumentDate { get; set; }
    public string ExternalDocumentNo { get; set; }
    public string Area { get; set; }
    public string TransactionSpecification { get; set; }
    public string PaymentMethodCode { get; set; }
    public string ShippingAgentCode { get; set; }
    public string PackageTrackingNo { get; set; }
    public string PreAssignedNoSeries { get; set; }
    public string NoSeries { get; set; }
    public string OrderNoSeries { get; set; }
    public string PreAssignedNo { get; set; }
    public string UserId { get; set; }
    public string SourceCode { get; set; }
    public string TaxAreaCode { get; set; }
    public bool TaxLiable { get; set; }
    public string VatBusPostingGroup { get; set; }
    public decimal VatBaseDiscount { get; set; }
    public string PrepaymentNoSeries { get; set; }
    public bool PrepaymentInvoice { get; set; }
    public string PrepaymentOrderNo { get; set; }
    public string QuoteNo { get; set; }
    public byte[]? WorkDescription { get; set; }
    public int DimensionSetId { get; set; }
    public int PaymentServiceSetId { get; set; }
    public string DocumentExchangeIdentifier { get; set; }
    public short DocumentExchangeStatus { get; set; }
    public string DocExchOriginalIdentifier { get; set; }
    public bool CoupledToCrm { get; set; }
    public string DirectDebitMandateId { get; set; }
    public int CustLedgerEntryNo { get; set; }
    public string CampaignNo { get; set; }
    public string SellToContactNo { get; set; }
    public string BillToContactNo { get; set; }
    public string OpportunityNo { get; set; }
    public string ResponsibilityCenter { get; set; }
    public bool AllowLineDisc { get; set; }
    public bool GetShipmentUsed { get; set; }

    public static OperationResult<SalesInvoiceHeader, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SalesInvoiceHeader, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SalesInvoiceHeader()
        {
            TenantId = tenantId
        };
        return OperationResult<SalesInvoiceHeader, DomainError>.Ok(entity);
    }
}
