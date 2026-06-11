using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ConfigSetupConfiguration : IEntityTypeConfiguration<ConfigSetup>
{
    public void Configure(EntityTypeBuilder<ConfigSetup> builder)
    {
        builder.ToTable("config_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Name2).HasColumnName("name_2");
        builder.Property(x => x.Address).HasColumnName("address");
        builder.Property(x => x.Address2).HasColumnName("address_2");
        builder.Property(x => x.City).HasColumnName("city");
        builder.Property(x => x.PhoneNo).HasColumnName("phone_no");
        builder.Property(x => x.PhoneNo2).HasColumnName("phone_no_2");
        builder.Property(x => x.TelexNo).HasColumnName("telex_no");
        builder.Property(x => x.FaxNo).HasColumnName("fax_no");
        builder.Property(x => x.GiroNo).HasColumnName("giro_no");
        builder.Property(x => x.BankName).HasColumnName("bank_name");
        builder.Property(x => x.BankBranchNo).HasColumnName("bank_branch_no");
        builder.Property(x => x.BankAccountNo).HasColumnName("bank_account_no");
        builder.Property(x => x.PaymentRoutingNo).HasColumnName("payment_routing_no");
        builder.Property(x => x.CustomsPermitNo).HasColumnName("customs_permit_no");
        builder.Property(x => x.CustomsPermitDate).HasColumnName("customs_permit_date");
        builder.Property(x => x.VatRegistrationNo).HasColumnName("vat_registration_no");
        builder.Property(x => x.RegistrationNo).HasColumnName("registration_no");
        builder.Property(x => x.TelexAnswerBack).HasColumnName("telex_answer_back");
        builder.Property(x => x.ShipToName).HasColumnName("ship_to_name");
        builder.Property(x => x.ShipToName2).HasColumnName("ship_to_name_2");
        builder.Property(x => x.ShipToAddress).HasColumnName("ship_to_address");
        builder.Property(x => x.ShipToAddress2).HasColumnName("ship_to_address_2");
        builder.Property(x => x.ShipToCity).HasColumnName("ship_to_city");
        builder.Property(x => x.ShipToContact).HasColumnName("ship_to_contact");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.Picture).HasColumnName("picture");
        builder.Property(x => x.PostCode).HasColumnName("post_code");
        builder.Property(x => x.County).HasColumnName("county");
        builder.Property(x => x.ShipToPostCode).HasColumnName("ship_to_post_code");
        builder.Property(x => x.ShipToCounty).HasColumnName("ship_to_county");
        builder.Property(x => x.EMail).HasColumnName("e_mail");
        builder.Property(x => x.HomePage).HasColumnName("home_page");
        builder.Property(x => x.CountryRegionCode).HasColumnName("country_region_code");
        builder.Property(x => x.ShipToCountryRegionCode).HasColumnName("ship_to_country_region_code");
        builder.Property(x => x.Iban).HasColumnName("iban");
        builder.Property(x => x.SwiftCode).HasColumnName("swift_code");
        builder.Property(x => x.IndustrialClassification).HasColumnName("industrial_classification");
        builder.Property(x => x.LogoPositionOnDocuments).HasColumnName("logo_position_on_documents");
        builder.Property(x => x.ResponsibilityCenter).HasColumnName("responsibility_center");
        builder.Property(x => x.CheckAvailPeriodCalc).HasColumnName("check_avail_period_calc");
        builder.Property(x => x.CheckAvailTimeBucket).HasColumnName("check_avail_time_bucket");
        builder.Property(x => x.BaseCalendarCode).HasColumnName("base_calendar_code");
        builder.Property(x => x.CalConvergenceTimeFrame).HasColumnName("cal_convergence_time_frame");
        builder.Property(x => x.PackageFileName).HasColumnName("package_file_name");
        builder.Property(x => x.PackageCode).HasColumnName("package_code");
        builder.Property(x => x.LanguageId).HasColumnName("language_id");
        builder.Property(x => x.ProductVersion).HasColumnName("product_version");
        builder.Property(x => x.PackageName).HasColumnName("package_name");
        builder.Property(x => x.YourProfileCode).HasColumnName("your_profile_code");
    }
}
