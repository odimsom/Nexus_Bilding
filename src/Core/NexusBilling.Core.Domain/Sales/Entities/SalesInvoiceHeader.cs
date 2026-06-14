using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class SalesInvoiceHeader : Entity
{
    private SalesInvoiceHeader() { }

    public TenantIdentifier TenantId { get; set; }
    public string SellToCustomerNo { get; set; } = string.Empty;
    public string No { get; set; } = string.Empty;
    public string BillToCustomerNo { get; set; } = string.Empty;
    public string BillToName { get; set; } = string.Empty;
    public string BillToName2 { get; set; } = string.Empty;
    public string BillToAddress { get; set; } = string.Empty;
    public string BillToAddress2 { get; set; } = string.Empty;
    public string BillToCity { get; set; } = string.Empty;
    public string BillToContact { get; set; } = string.Empty;
    public string YourReference { get; set; } = string.Empty;
    public string ShipToCode { get; set; } = string.Empty;
    public string ShipToName { get; set; } = string.Empty;
    public string ShipToName2 { get; set; } = string.Empty;
    public string ShipToAddress { get; set; } = string.Empty;
    public string ShipToAddress2 { get; set; } = string.Empty;
    public string ShipToCity { get; set; } = string.Empty;
    public string ShipToContact { get; set; } = string.Empty;
    public DateTime? OrderDate { get; set; }
    public DateTime? PostingDate { get; set; }
    public DateTime? ShipmentDate { get; set; }
    public string PostingDescription { get; set; } = string.Empty;
    public string PaymentTermsCode { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public decimal Amount { get; set; }
    public decimal AmountIncludingVat { get; set; }
    public decimal PaymentDiscount { get; set; }
    public DateTime? PmtDiscountDate { get; set; }
    public string ShipmentMethodCode { get; set; } = string.Empty;
    public string LocationCode { get; set; } = string.Empty;
    public string ShortcutDimension1Code { get; set; } = string.Empty;
    public string ShortcutDimension2Code { get; set; } = string.Empty;
    public string CustomerPostingGroup { get; set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
    public decimal CurrencyFactor { get; set; }
    public string CustomerPriceGroup { get; set; } = string.Empty;
    public bool PricesIncludingVat { get; set; }
    public string InvoiceDiscCode { get; set; } = string.Empty;
    public string CustomerDiscGroup { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = string.Empty;
    public string SalespersonCode { get; set; } = string.Empty;
    public string OrderNo { get; set; } = string.Empty;
    public int NoPrinted { get; set; }
    public string OnHold { get; set; } = string.Empty;
    public short AppliesToDocType { get; set; }
    public string AppliesToDocNo { get; set; } = string.Empty;
    public string BalAccountNo { get; set; } = string.Empty;
    public string VatRegistrationNo { get; set; } = string.Empty;
    public string ReasonCode { get; set; } = string.Empty;
    public string GenBusPostingGroup { get; set; } = string.Empty;
    public bool Eu3PartyTrade { get; set; }
    public string TransactionType { get; set; } = string.Empty;
    public string TransportMethod { get; set; } = string.Empty;
    public string VatCountryRegionCode { get; set; } = string.Empty;
    public string SellToCustomerName { get; set; } = string.Empty;
    public string SellToCustomerName2 { get; set; } = string.Empty;
    public string SellToAddress { get; set; } = string.Empty;
    public string SellToAddress2 { get; set; } = string.Empty;
    public string SellToCity { get; set; } = string.Empty;
    public string SellToContact { get; set; } = string.Empty;
    public string BillToPostCode { get; set; } = string.Empty;
    public string BillToCounty { get; set; } = string.Empty;
    public string BillToCountryRegionCode { get; set; } = string.Empty;
    public string SellToPostCode { get; set; } = string.Empty;
    public string SellToCounty { get; set; } = string.Empty;
    public string SellToCountryRegionCode { get; set; } = string.Empty;
    public string ShipToPostCode { get; set; } = string.Empty;
    public string ShipToCounty { get; set; } = string.Empty;
    public string ShipToCountryRegionCode { get; set; } = string.Empty;
    public short BalAccountType { get; set; }
    public string ExitPoint { get; set; } = string.Empty;
    public bool Correction { get; set; }
    public DateTime? DocumentDate { get; set; }
    public string ExternalDocumentNo { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string TransactionSpecification { get; set; } = string.Empty;
    public string PaymentMethodCode { get; set; } = string.Empty;
    public string ShippingAgentCode { get; set; } = string.Empty;
    public string PackageTrackingNo { get; set; } = string.Empty;
    public string PreAssignedNoSeries { get; set; } = string.Empty;
    public string NoSeries { get; set; } = string.Empty;
    public string OrderNoSeries { get; set; } = string.Empty;
    public string PreAssignedNo { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string SourceCode { get; set; } = string.Empty;
    public string TaxAreaCode { get; set; } = string.Empty;
    public bool TaxLiable { get; set; }
    public string VatBusPostingGroup { get; set; } = string.Empty;
    public decimal VatBaseDiscount { get; set; }
    public string PrepaymentNoSeries { get; set; } = string.Empty;
    public bool PrepaymentInvoice { get; set; }
    public string PrepaymentOrderNo { get; set; } = string.Empty;
    public string QuoteNo { get; set; } = string.Empty;
    public byte[]? WorkDescription { get; set; }
    public int DimensionSetId { get; set; }
    public int PaymentServiceSetId { get; set; }
    public string DocumentExchangeIdentifier { get; set; } = string.Empty;
    public short DocumentExchangeStatus { get; set; }
    public string DocExchOriginalIdentifier { get; set; } = string.Empty;
    public bool CoupledToCrm { get; set; }
    public string DirectDebitMandateId { get; set; } = string.Empty;
    public int CustLedgerEntryNo { get; set; }
    public string CampaignNo { get; set; } = string.Empty;
    public string SellToContactNo { get; set; } = string.Empty;
    public string BillToContactNo { get; set; } = string.Empty;
    public string OpportunityNo { get; set; } = string.Empty;
    public string ResponsibilityCenter { get; set; } = string.Empty;
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
