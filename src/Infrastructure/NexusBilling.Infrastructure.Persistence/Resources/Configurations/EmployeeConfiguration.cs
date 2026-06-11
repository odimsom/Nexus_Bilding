using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employee", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.FirstName).HasColumnName("first_name");
        builder.Property(x => x.MiddleName).HasColumnName("middle_name");
        builder.Property(x => x.LastName).HasColumnName("last_name");
        builder.Property(x => x.Initials).HasColumnName("initials");
        builder.Property(x => x.JobTitle).HasColumnName("job_title");
        builder.Property(x => x.SearchName).HasColumnName("search_name");
        builder.Property(x => x.Address).HasColumnName("address");
        builder.Property(x => x.Address2).HasColumnName("address_2");
        builder.Property(x => x.City).HasColumnName("city");
        builder.Property(x => x.PostCode).HasColumnName("post_code");
        builder.Property(x => x.County).HasColumnName("county");
        builder.Property(x => x.PhoneNo).HasColumnName("phone_no");
        builder.Property(x => x.MobilePhoneNo).HasColumnName("mobile_phone_no");
        builder.Property(x => x.EMail).HasColumnName("e_mail");
        builder.Property(x => x.AltAddressCode).HasColumnName("alt_address_code");
        builder.Property(x => x.AltAddressStartDate).HasColumnName("alt_address_start_date");
        builder.Property(x => x.AltAddressEndDate).HasColumnName("alt_address_end_date");
        builder.Property(x => x.Picture).HasColumnName("picture");
        builder.Property(x => x.BirthDate).HasColumnName("birth_date");
        builder.Property(x => x.SocialSecurityNo).HasColumnName("social_security_no");
        builder.Property(x => x.UnionCode).HasColumnName("union_code");
        builder.Property(x => x.UnionMembershipNo).HasColumnName("union_membership_no");
        builder.Property(x => x.Gender).HasColumnName("gender");
        builder.Property(x => x.CountryRegionCode).HasColumnName("country_region_code");
        builder.Property(x => x.ManagerNo).HasColumnName("manager_no");
        builder.Property(x => x.EmplymtContractCode).HasColumnName("emplymt_contract_code");
        builder.Property(x => x.StatisticsGroupCode).HasColumnName("statistics_group_code");
        builder.Property(x => x.EmploymentDate).HasColumnName("employment_date");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.InactiveDate).HasColumnName("inactive_date");
        builder.Property(x => x.CauseOfInactivityCode).HasColumnName("cause_of_inactivity_code");
        builder.Property(x => x.TerminationDate).HasColumnName("termination_date");
        builder.Property(x => x.GroundsForTermCode).HasColumnName("grounds_for_term_code");
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension_1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension_2_code");
        builder.Property(x => x.ResourceNo).HasColumnName("resource_no");
        builder.Property(x => x.LastDateModified).HasColumnName("last_date_modified");
        builder.Property(x => x.Extension).HasColumnName("extension");
        builder.Property(x => x.Pager).HasColumnName("pager");
        builder.Property(x => x.FaxNo).HasColumnName("fax_no");
        builder.Property(x => x.CompanyEMail).HasColumnName("company_e_mail");
        builder.Property(x => x.Title).HasColumnName("title");
        builder.Property(x => x.SalespersPurchCode).HasColumnName("salespers_purch_code");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.Image).HasColumnName("image");
        builder.Property(x => x.CostCenterCode).HasColumnName("cost_center_code");
        builder.Property(x => x.CostObjectCode).HasColumnName("cost_object_code");
    }
}
