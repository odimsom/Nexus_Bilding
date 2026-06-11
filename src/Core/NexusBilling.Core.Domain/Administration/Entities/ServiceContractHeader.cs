using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceContractHeader : Entity
{
    private ServiceContractHeader() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ContractNo { get; private set; }
    public short ContractType { get; private set; }
    public string Description { get; private set; }
    public string Description2 { get; private set; }
    public short Status { get; private set; }
    public short ChangeStatus { get; private set; }
    public string CustomerNo { get; private set; }
    public string ContactName { get; private set; }
    public string YourReference { get; private set; }
    public string SalespersonCode { get; private set; }
    public string BillToCustomerNo { get; private set; }
    public string ShipToCode { get; private set; }
    public string ServContractAccGrCode { get; private set; }
    public short InvoicePeriod { get; private set; }
    public DateTime? LastInvoiceDate { get; private set; }
    public DateTime? NextInvoiceDate { get; private set; }
    public DateTime? StartingDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public DateTime? FirstServiceDate { get; private set; }
    public decimal MaxLaborUnitPrice { get; private set; }
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
    public DateTime? NextInvoicePeriodStart { get; private set; }
    public DateTime? NextInvoicePeriodEnd { get; private set; }
    public int DimensionSetId { get; private set; }
    public string ContactNo { get; private set; }
    public string BillToContactNo { get; private set; }
    public string BillToContact { get; private set; }
    public DateTime? LastInvoicePeriodEnd { get; private set; }

    public static OperationResult<ServiceContractHeader, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServiceContractHeader, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServiceContractHeader()
        {
            TenantId = tenantId
        };
        return OperationResult<ServiceContractHeader, DomainError>.Ok(entity);
    }
}
