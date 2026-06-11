using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ConfigSetup : Entity
{
    private ConfigSetup() { }

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
    public short LogoPositionOnDocuments { get; private set; }
    public string ResponsibilityCenter { get; private set; }
    public string CheckAvailPeriodCalc { get; private set; }
    public short CheckAvailTimeBucket { get; private set; }
    public string BaseCalendarCode { get; private set; }
    public string CalConvergenceTimeFrame { get; private set; }
    public string PackageFileName { get; private set; }
    public string PackageCode { get; private set; }
    public int LanguageId { get; private set; }
    public string ProductVersion { get; private set; }
    public string PackageName { get; private set; }
    public string YourProfileCode { get; private set; }

    public static OperationResult<ConfigSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ConfigSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ConfigSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<ConfigSetup, DomainError>.Ok(entity);
    }
}
