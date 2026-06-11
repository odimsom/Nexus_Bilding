using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class Contact : Entity
{
    private Contact() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string Name { get; private set; }
    public string SearchName { get; private set; }
    public string Name2 { get; private set; }
    public string Address { get; private set; }
    public string Address2 { get; private set; }
    public string City { get; private set; }
    public string PhoneNo { get; private set; }
    public string TelexNo { get; private set; }
    public string TerritoryCode { get; private set; }
    public string CurrencyCode { get; private set; }
    public string LanguageCode { get; private set; }
    public string SalespersonCode { get; private set; }
    public string CountryRegionCode { get; private set; }
    public DateTime? LastDateModified { get; private set; }
    public string FaxNo { get; private set; }
    public string TelexAnswerBack { get; private set; }
    public string VatRegistrationNo { get; private set; }
    public byte[]? Picture { get; private set; }
    public string PostCode { get; private set; }
    public string County { get; private set; }
    public string EMail { get; private set; }
    public string HomePage { get; private set; }
    public string NoSeries { get; private set; }
    public Guid Image { get; private set; }
    public short Type { get; private set; }
    public string CompanyNo { get; private set; }
    public string CompanyName { get; private set; }
    public string LookupContactNo { get; private set; }
    public string FirstName { get; private set; }
    public string MiddleName { get; private set; }
    public string Surname { get; private set; }
    public string JobTitle { get; private set; }
    public string Initials { get; private set; }
    public string ExtensionNo { get; private set; }
    public string MobilePhoneNo { get; private set; }
    public string Pager { get; private set; }
    public string OrganizationalLevelCode { get; private set; }
    public bool ExcludeFromSegment { get; private set; }
    public string ExternalId { get; private set; }
    public short CorrespondenceType { get; private set; }
    public string SalutationCode { get; private set; }
    public string SearchEMail { get; private set; }
    public string LastTimeModified { get; private set; }
    public string EMail2 { get; private set; }

    public static OperationResult<Contact, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<Contact, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new Contact()
        {
            TenantId = tenantId
        };
        return OperationResult<Contact, DomainError>.Ok(entity);
    }
}
