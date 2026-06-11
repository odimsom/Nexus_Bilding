using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class Employee : Entity
{
    private Employee() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string FirstName { get; private set; }
    public string MiddleName { get; private set; }
    public string LastName { get; private set; }
    public string Initials { get; private set; }
    public string JobTitle { get; private set; }
    public string SearchName { get; private set; }
    public string Address { get; private set; }
    public string Address2 { get; private set; }
    public string City { get; private set; }
    public string PostCode { get; private set; }
    public string County { get; private set; }
    public string PhoneNo { get; private set; }
    public string MobilePhoneNo { get; private set; }
    public string EMail { get; private set; }
    public string AltAddressCode { get; private set; }
    public DateTime? AltAddressStartDate { get; private set; }
    public DateTime? AltAddressEndDate { get; private set; }
    public byte[]? Picture { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public string SocialSecurityNo { get; private set; }
    public string UnionCode { get; private set; }
    public string UnionMembershipNo { get; private set; }
    public short Gender { get; private set; }
    public string CountryRegionCode { get; private set; }
    public string ManagerNo { get; private set; }
    public string EmplymtContractCode { get; private set; }
    public string StatisticsGroupCode { get; private set; }
    public DateTime? EmploymentDate { get; private set; }
    public short Status { get; private set; }
    public DateTime? InactiveDate { get; private set; }
    public string CauseOfInactivityCode { get; private set; }
    public DateTime? TerminationDate { get; private set; }
    public string GroundsForTermCode { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public string ResourceNo { get; private set; }
    public DateTime? LastDateModified { get; private set; }
    public string Extension { get; private set; }
    public string Pager { get; private set; }
    public string FaxNo { get; private set; }
    public string CompanyEMail { get; private set; }
    public string Title { get; private set; }
    public string SalespersPurchCode { get; private set; }
    public string NoSeries { get; private set; }
    public Guid Image { get; private set; }
    public string CostCenterCode { get; private set; }
    public string CostObjectCode { get; private set; }

    public static OperationResult<Employee, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<Employee, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new Employee()
        {
            TenantId = tenantId
        };
        return OperationResult<Employee, DomainError>.Ok(entity);
    }
}
