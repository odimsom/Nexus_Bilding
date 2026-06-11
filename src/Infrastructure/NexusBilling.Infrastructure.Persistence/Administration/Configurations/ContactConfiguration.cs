using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("contact", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.SearchName).HasColumnName("search_name");
        builder.Property(x => x.Name2).HasColumnName("name_2");
        builder.Property(x => x.Address).HasColumnName("address");
        builder.Property(x => x.Address2).HasColumnName("address_2");
        builder.Property(x => x.City).HasColumnName("city");
        builder.Property(x => x.PhoneNo).HasColumnName("phone_no");
        builder.Property(x => x.TelexNo).HasColumnName("telex_no");
        builder.Property(x => x.TerritoryCode).HasColumnName("territory_code");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.LanguageCode).HasColumnName("language_code");
        builder.Property(x => x.SalespersonCode).HasColumnName("salesperson_code");
        builder.Property(x => x.CountryRegionCode).HasColumnName("country_region_code");
        builder.Property(x => x.LastDateModified).HasColumnName("last_date_modified");
        builder.Property(x => x.FaxNo).HasColumnName("fax_no");
        builder.Property(x => x.TelexAnswerBack).HasColumnName("telex_answer_back");
        builder.Property(x => x.VatRegistrationNo).HasColumnName("vat_registration_no");
        builder.Property(x => x.Picture).HasColumnName("picture");
        builder.Property(x => x.PostCode).HasColumnName("post_code");
        builder.Property(x => x.County).HasColumnName("county");
        builder.Property(x => x.EMail).HasColumnName("e_mail");
        builder.Property(x => x.HomePage).HasColumnName("home_page");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.Image).HasColumnName("image");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.CompanyNo).HasColumnName("company_no");
        builder.Property(x => x.CompanyName).HasColumnName("company_name");
        builder.Property(x => x.LookupContactNo).HasColumnName("lookup_contact_no");
        builder.Property(x => x.FirstName).HasColumnName("first_name");
        builder.Property(x => x.MiddleName).HasColumnName("middle_name");
        builder.Property(x => x.Surname).HasColumnName("surname");
        builder.Property(x => x.JobTitle).HasColumnName("job_title");
        builder.Property(x => x.Initials).HasColumnName("initials");
        builder.Property(x => x.ExtensionNo).HasColumnName("extension_no");
        builder.Property(x => x.MobilePhoneNo).HasColumnName("mobile_phone_no");
        builder.Property(x => x.Pager).HasColumnName("pager");
        builder.Property(x => x.OrganizationalLevelCode).HasColumnName("organizational_level_code");
        builder.Property(x => x.ExcludeFromSegment).HasColumnName("exclude_from_segment");
        builder.Property(x => x.ExternalId).HasColumnName("external_id");
        builder.Property(x => x.CorrespondenceType).HasColumnName("correspondence_type");
        builder.Property(x => x.SalutationCode).HasColumnName("salutation_code");
        builder.Property(x => x.SearchEMail).HasColumnName("search_e_mail");
        builder.Property(x => x.LastTimeModified).HasColumnName("last_time_modified");
        builder.Property(x => x.EMail2).HasColumnName("e_mail_2");
    }
}
