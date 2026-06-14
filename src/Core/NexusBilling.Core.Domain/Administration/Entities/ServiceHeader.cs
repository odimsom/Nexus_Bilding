using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceHeader : Entity
{
    private ServiceHeader() { }

    public TenantIdentifier TenantId { get; private set; }
    public short DocumentType { get; set; }
    public string CustomerNo { get; set; } = string.Empty;
    public string No { get; set; } = string.Empty;
    public string BillToCustomerNo { get; set; } = string.Empty;
    public string BillToName { get; set; } = string.Empty;
    public string BillToName2 { get; private set; } = string.Empty;
    public string BillToAddress { get; private set; } = string.Empty;
    public string BillToAddress2 { get; private set; } = string.Empty;
    public string BillToCity { get; private set; } = string.Empty;
    public string BillToContact { get; private set; } = string.Empty;
    public string YourReference { get; private set; } = string.Empty;
    public string ShipToCode { get; private set; } = string.Empty;
    public string ShipToName { get; private set; } = string.Empty;
    public string ShipToName2 { get; private set; } = string.Empty;
    public string ShipToAddress { get; private set; } = string.Empty;
    public string ShipToAddress2 { get; private set; } = string.Empty;
    public string ShipToCity { get; private set; } = string.Empty;
    public string ShipToContact { get; private set; } = string.Empty;
    public DateTime? OrderDate { get; set; }
    public DateTime? PostingDate { get; set; }
    public string PostingDescription { get; set; } = string.Empty;
    public string PaymentTermsCode { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public decimal PaymentDiscount { get; private set; }
    public DateTime? PmtDiscountDate { get; private set; }
    public string ShipmentMethodCode { get; private set; } = string.Empty;
    public string LocationCode { get; private set; } = string.Empty;
    public string ShortcutDimension1Code { get; private set; } = string.Empty;
    public string ShortcutDimension2Code { get; private set; } = string.Empty;
    public string CustomerPostingGroup { get; private set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
    public decimal CurrencyFactor { get; private set; }
    public string CustomerPriceGroup { get; private set; } = string.Empty;
    public bool PricesIncludingVat { get; private set; }
    public string InvoiceDiscCode { get; private set; } = string.Empty;
    public string CustomerDiscGroup { get; private set; } = string.Empty;
    public string LanguageCode { get; private set; } = string.Empty;
    public string SalespersonCode { get; set; } = string.Empty;
    public int NoPrinted { get; private set; }
    public short AppliesToDocType { get; private set; }
    public string AppliesToDocNo { get; private set; } = string.Empty;
    public string BalAccountNo { get; private set; } = string.Empty;
    public string ShippingNo { get; private set; } = string.Empty;
    public string PostingNo { get; private set; } = string.Empty;
    public string LastShippingNo { get; private set; } = string.Empty;
    public string LastPostingNo { get; private set; } = string.Empty;
    public string VatRegistrationNo { get; private set; } = string.Empty;
    public string ReasonCode { get; private set; } = string.Empty;
    public string GenBusPostingGroup { get; private set; } = string.Empty;
    public bool Eu3PartyTrade { get; private set; }
    public string TransactionType { get; private set; } = string.Empty;
    public string TransportMethod { get; private set; } = string.Empty;
    public string VatCountryRegionCode { get; private set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Name2 { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string Address2 { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string ContactName { get; private set; } = string.Empty;
    public string BillToPostCode { get; private set; } = string.Empty;
    public string BillToCounty { get; private set; } = string.Empty;
    public string BillToCountryRegionCode { get; private set; } = string.Empty;
    public string PostCode { get; private set; } = string.Empty;
    public string County { get; private set; } = string.Empty;
    public string CountryRegionCode { get; private set; } = string.Empty;
    public string ShipToPostCode { get; private set; } = string.Empty;
    public string ShipToCounty { get; private set; } = string.Empty;
    public string ShipToCountryRegionCode { get; private set; } = string.Empty;
    public short BalAccountType { get; private set; }
    public string ExitPoint { get; private set; } = string.Empty;
    public bool Correction { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public string Area { get; private set; } = string.Empty;
    public string TransactionSpecification { get; private set; } = string.Empty;
    public string PaymentMethodCode { get; set; } = string.Empty;
    public string ShippingAgentCode { get; private set; } = string.Empty;
    public string NoSeries { get; private set; } = string.Empty;
    public string PostingNoSeries { get; private set; } = string.Empty;
    public string ShippingNoSeries { get; private set; } = string.Empty;
    public string TaxAreaCode { get; private set; } = string.Empty;
    public bool TaxLiable { get; private set; }
    public string VatBusPostingGroup { get; private set; } = string.Empty;
    public short Reserve { get; private set; }
    public string AppliesToId { get; private set; } = string.Empty;
    public decimal VatBaseDiscount { get; private set; }
    public short Status { get; set; }
    public short InvoiceDiscountCalculation { get; private set; }
    public decimal InvoiceDiscountValue { get; private set; }
    public short ReleaseStatus { get; private set; }
    public int DimensionSetId { get; private set; }
    public string ContactNo { get; private set; } = string.Empty;
    public string BillToContactNo { get; private set; } = string.Empty;
    public string ResponsibilityCenter { get; private set; } = string.Empty;
    public short ShippingAdvice { get; private set; }
    public string ShippingTime { get; private set; } = string.Empty;
    public string ShippingAgentServiceCode { get; private set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ServiceOrderType { get; private set; } = string.Empty;
    public bool LinkServiceToServiceItem { get; private set; }
    public short Priority { get; private set; }
    public string PhoneNo { get; private set; } = string.Empty;
    public string EMail { get; private set; } = string.Empty;
    public string PhoneNo2 { get; private set; } = string.Empty;
    public string FaxNo { get; private set; } = string.Empty;
    public string OrderTime { get; private set; } = string.Empty;
    public decimal DefaultResponseTimeHours { get; private set; }
    public decimal ActualResponseTimeHours { get; private set; }
    public decimal ServiceTimeHours { get; private set; }
    public DateTime? ResponseDate { get; private set; }
    public string ResponseTime { get; private set; } = string.Empty;
    public DateTime? StartingDate { get; set; }
    public string StartingTime { get; private set; } = string.Empty;
    public DateTime? FinishingDate { get; set; }
    public string FinishingTime { get; private set; } = string.Empty;
    public short NotifyCustomer { get; private set; }
    public decimal MaxLaborUnitPrice { get; private set; }
    public short WarningStatus { get; private set; }
    public string ContractNo { get; set; } = string.Empty;
    public string ShipToFaxNo { get; private set; } = string.Empty;
    public string ShipToEMail { get; private set; } = string.Empty;
    public string ShipToPhone { get; private set; } = string.Empty;
    public string ShipToPhone2 { get; private set; } = string.Empty;
    public string ServiceZoneCode { get; private set; } = string.Empty;
    public DateTime? ExpectedFinishingDate { get; private set; }
    public bool AllowLineDisc { get; private set; }
    public string AssignedUserId { get; private set; } = string.Empty;
    public string QuoteNo { get; private set; } = string.Empty;

    public static OperationResult<ServiceHeader, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServiceHeader, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServiceHeader()
        {
            TenantId = tenantId
        };
        return OperationResult<ServiceHeader, DomainError>.Ok(entity);
    }
}
