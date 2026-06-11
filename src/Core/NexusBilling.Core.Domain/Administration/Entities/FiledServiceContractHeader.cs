using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class FiledServiceContractHeader : Entity
{
    private FiledServiceContractHeader() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ContractNo { get; private set; }
    public short ContractType { get; private set; }
    public string Description { get; private set; }
    public string Description2 { get; private set; }
    public short Status { get; private set; }
    public short ChangeStatus { get; private set; }
    public string CustomerNo { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string Address2 { get; private set; }
    public string PostCode { get; private set; }
    public string City { get; private set; }
    public string ContactName { get; private set; }
    public string YourReference { get; private set; }
    public string SalespersonCode { get; private set; }
    public string BillToCustomerNo { get; private set; }
    public string BillToName { get; private set; }
    public string BillToAddress { get; private set; }
    public string BillToAddress2 { get; private set; }
    public string BillToPostCode { get; private set; }
    public string BillToCity { get; private set; }
    public string ShipToCode { get; private set; }
    public string ShipToName { get; private set; }
    public string ShipToAddress { get; private set; }
    public string ShipToAddress2 { get; private set; }
    public string ShipToPostCode { get; private set; }
    public string ShipToCity { get; private set; }
    public string ServContractAccGrCode { get; private set; }
    public short InvoicePeriod { get; private set; }
    public DateTime? LastInvoiceDate { get; private set; }
    public DateTime? NextInvoiceDate { get; private set; }
    public DateTime? StartingDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public DateTime? FirstServiceDate { get; private set; }
    public decimal MaxLaborUnitPrice { get; private set; }
    public decimal CalcdAnnualAmount { get; private set; }
    public decimal AnnualAmount { get; private set; }
    public decimal AmountPerPeriod { get; private set; }
    public bool CombineInvoices { get; private set; }
    public bool Prepaid { get; private set; }
    public string NextInvoicePeriod { get; private set; }
    public string ServiceZoneCode { get; private set; }
    public string LanguageCode { get; private set; }
    public string CancelReasonCode { get; private set; }
    public DateTime? LastPriceUpdateDate { get; private set; }
    public DateTime? NextPriceUpdateDate { get; private set; }
    public decimal LastPriceUpdate { get; private set; }
    public decimal ResponseTimeHours { get; private set; }
    public bool ContractLinesOnInvoice { get; private set; }
    public string ServicePeriod { get; private set; }
    public string PaymentTermsCode { get; private set; }
    public bool InvoiceAfterService { get; private set; }
    public short QuoteType { get; private set; }
    public bool AllowUnbalancedAmounts { get; private set; }
    public string ContractGroupCode { get; private set; }
    public string ServiceOrderType { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public DateTime? AcceptBefore { get; private set; }
    public bool AutomaticCreditMemos { get; private set; }
    public string TemplateNo { get; private set; }
    public string PriceUpdatePeriod { get; private set; }
    public string PriceInvIncreaseCode { get; private set; }
    public bool PrintIncreaseText { get; private set; }
    public string CurrencyCode { get; private set; }
    public string NoSeries { get; private set; }
    public decimal Probability { get; private set; }
    public string ResponsibilityCenter { get; private set; }
    public string PhoneNo { get; private set; }
    public string FaxNo { get; private set; }
    public string EMail { get; private set; }
    public string BillToCounty { get; private set; }
    public string County { get; private set; }
    public string ShipToCounty { get; private set; }
    public string CountryRegionCode { get; private set; }
    public string BillToCountryRegionCode { get; private set; }
    public string ShipToCountryRegionCode { get; private set; }
    public string Name2 { get; private set; }
    public string BillToName2 { get; private set; }
    public string ShipToName2 { get; private set; }
    public DateTime? NextInvoicePeriodStart { get; private set; }
    public DateTime? NextInvoicePeriodEnd { get; private set; }
    public int EntryNo { get; private set; }
    public DateTime? FileDate { get; private set; }
    public string FileTime { get; private set; }
    public string FiledBy { get; private set; }
    public short ReasonForFiling { get; private set; }
    public short ContractTypeRelation { get; private set; }
    public string ContractNoRelation { get; private set; }
    public int DimensionSetId { get; private set; }
    public string ContactNo { get; private set; }
    public string BillToContactNo { get; private set; }
    public string BillToContact { get; private set; }

    public static OperationResult<FiledServiceContractHeader, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FiledServiceContractHeader, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FiledServiceContractHeader()
        {
            TenantId = tenantId
        };
        return OperationResult<FiledServiceContractHeader, DomainError>.Ok(entity);
    }
}
