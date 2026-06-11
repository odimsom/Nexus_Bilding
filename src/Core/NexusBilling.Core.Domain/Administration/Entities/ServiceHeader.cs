using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceHeader : Entity
{
    private ServiceHeader() { }

    public TenantIdentifier TenantId { get; private set; }
    public short DocumentType { get; private set; }
    public string CustomerNo { get; private set; }
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
    public int NoPrinted { get; private set; }
    public short AppliesToDocType { get; private set; }
    public string AppliesToDocNo { get; private set; }
    public string BalAccountNo { get; private set; }
    public string ShippingNo { get; private set; }
    public string PostingNo { get; private set; }
    public string LastShippingNo { get; private set; }
    public string LastPostingNo { get; private set; }
    public string VatRegistrationNo { get; private set; }
    public string ReasonCode { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public bool Eu3PartyTrade { get; private set; }
    public string TransactionType { get; private set; }
    public string TransportMethod { get; private set; }
    public string VatCountryRegionCode { get; private set; }
    public string Name { get; private set; }
    public string Name2 { get; private set; }
    public string Address { get; private set; }
    public string Address2 { get; private set; }
    public string City { get; private set; }
    public string ContactName { get; private set; }
    public string BillToPostCode { get; private set; }
    public string BillToCounty { get; private set; }
    public string BillToCountryRegionCode { get; private set; }
    public string PostCode { get; private set; }
    public string County { get; private set; }
    public string CountryRegionCode { get; private set; }
    public string ShipToPostCode { get; private set; }
    public string ShipToCounty { get; private set; }
    public string ShipToCountryRegionCode { get; private set; }
    public short BalAccountType { get; private set; }
    public string ExitPoint { get; private set; }
    public bool Correction { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public string Area { get; private set; }
    public string TransactionSpecification { get; private set; }
    public string PaymentMethodCode { get; private set; }
    public string ShippingAgentCode { get; private set; }
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
    public short ReleaseStatus { get; private set; }
    public int DimensionSetId { get; private set; }
    public string ContactNo { get; private set; }
    public string BillToContactNo { get; private set; }
    public string ResponsibilityCenter { get; private set; }
    public short ShippingAdvice { get; private set; }
    public string ShippingTime { get; private set; }
    public string ShippingAgentServiceCode { get; private set; }
    public string Description { get; private set; }
    public string ServiceOrderType { get; private set; }
    public bool LinkServiceToServiceItem { get; private set; }
    public short Priority { get; private set; }
    public string PhoneNo { get; private set; }
    public string EMail { get; private set; }
    public string PhoneNo2 { get; private set; }
    public string FaxNo { get; private set; }
    public string OrderTime { get; private set; }
    public decimal DefaultResponseTimeHours { get; private set; }
    public decimal ActualResponseTimeHours { get; private set; }
    public decimal ServiceTimeHours { get; private set; }
    public DateTime? ResponseDate { get; private set; }
    public string ResponseTime { get; private set; }
    public DateTime? StartingDate { get; private set; }
    public string StartingTime { get; private set; }
    public DateTime? FinishingDate { get; private set; }
    public string FinishingTime { get; private set; }
    public short NotifyCustomer { get; private set; }
    public decimal MaxLaborUnitPrice { get; private set; }
    public short WarningStatus { get; private set; }
    public string ContractNo { get; private set; }
    public string ShipToFaxNo { get; private set; }
    public string ShipToEMail { get; private set; }
    public string ShipToPhone { get; private set; }
    public string ShipToPhone2 { get; private set; }
    public string ServiceZoneCode { get; private set; }
    public DateTime? ExpectedFinishingDate { get; private set; }
    public bool AllowLineDisc { get; private set; }
    public string AssignedUserId { get; private set; }
    public string QuoteNo { get; private set; }

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
