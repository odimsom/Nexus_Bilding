using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class CompanyInformation : Entity
{
    private CompanyInformation() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string Name { get; private set; }
    public string Name2 { get; private set; }
    public string Address { get; private set; }
    public string Address2 { get; private set; }
    public string City { get; private set; }
    public string PhoneNo { get; private set; }
    public string PhoneNo2 { get; private set; }
    public string TelexNo { get; private set; }
    public string FaxNo { get; private set; }
    public string GiroNo { get; private set; }
    public string BankName { get; private set; }
    public string BankBranchNo { get; private set; }
    public string BankAccountNo { get; private set; }
    public string PaymentRoutingNo { get; private set; }
    public string CustomsPermitNo { get; private set; }
    public DateTime? CustomsPermitDate { get; private set; }
    public string VatRegistrationNo { get; private set; }
    public string RegistrationNo { get; private set; }
    public string TelexAnswerBack { get; private set; }
    public string ShipToName { get; private set; }
    public string ShipToName2 { get; private set; }
    public string ShipToAddress { get; private set; }
    public string ShipToAddress2 { get; private set; }
    public string ShipToCity { get; private set; }
    public string ShipToContact { get; private set; }
    public string LocationCode { get; private set; }
    public byte[]? Picture { get; private set; }
    public string PostCode { get; private set; }
    public string County { get; private set; }
    public string ShipToPostCode { get; private set; }
    public string ShipToCounty { get; private set; }
    public string EMail { get; private set; }
    public string HomePage { get; private set; }
    public string CountryRegionCode { get; private set; }
    public string ShipToCountryRegionCode { get; private set; }
    public string Iban { get; private set; }
    public string SwiftCode { get; private set; }
    public string IndustrialClassification { get; private set; }
    public string IcPartnerCode { get; private set; }
    public short IcInboxType { get; private set; }
    public string IcInboxDetails { get; private set; }
    public short SystemIndicator { get; private set; }
    public string CustomSystemIndicatorText { get; private set; }
    public short SystemIndicatorStyle { get; private set; }
    public bool AllowBlankPaymentInfo { get; private set; }
    public string Gln { get; private set; }
    public DateTime? CreatedDatetime { get; private set; }
    public bool DemoCompany { get; private set; }
    public string ResponsibilityCenter { get; private set; }
    public string CheckAvailPeriodCalc { get; private set; }
    public short CheckAvailTimeBucket { get; private set; }
    public string BaseCalendarCode { get; private set; }
    public string CalConvergenceTimeFrame { get; private set; }
    public bool ShowChartOnRolecenter { get; private set; }

    public static OperationResult<CompanyInformation, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CompanyInformation, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CompanyInformation()
        {
            TenantId = tenantId
        };
        return OperationResult<CompanyInformation, DomainError>.Ok(entity);
    }
}
