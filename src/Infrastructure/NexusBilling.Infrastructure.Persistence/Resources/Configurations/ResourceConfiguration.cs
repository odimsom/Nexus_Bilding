using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.ToTable("resource", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.SearchName).HasColumnName("search_name");
        builder.Property(x => x.Name2).HasColumnName("name_2");
        builder.Property(x => x.Address).HasColumnName("address");
        builder.Property(x => x.Address2).HasColumnName("address_2");
        builder.Property(x => x.City).HasColumnName("city");
        builder.Property(x => x.SocialSecurityNo).HasColumnName("social_security_no");
        builder.Property(x => x.JobTitle).HasColumnName("job_title");
        builder.Property(x => x.Education).HasColumnName("education");
        builder.Property(x => x.ContractClass).HasColumnName("contract_class");
        builder.Property(x => x.EmploymentDate).HasColumnName("employment_date");
        builder.Property(x => x.ResourceGroupNo).HasColumnName("resource_group_no");
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension_1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension_2_code");
        builder.Property(x => x.BaseUnitOfMeasure).HasColumnName("base_unit_of_measure");
        builder.Property(x => x.DirectUnitCost).HasColumnName("direct_unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.IndirectCost).HasColumnName("indirect_cost").HasPrecision(18, 5);
        builder.Property(x => x.UnitCost).HasColumnName("unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.Profit).HasColumnName("profit").HasPrecision(18, 5);
        builder.Property(x => x.PriceProfitCalculation).HasColumnName("price_profit_calculation");
        builder.Property(x => x.UnitPrice).HasColumnName("unit_price").HasPrecision(18, 5);
        builder.Property(x => x.VendorNo).HasColumnName("vendor_no");
        builder.Property(x => x.LastDateModified).HasColumnName("last_date_modified");
        builder.Property(x => x.Blocked).HasColumnName("blocked");
        builder.Property(x => x.GenProdPostingGroup).HasColumnName("gen_prod_posting_group");
        builder.Property(x => x.Picture).HasColumnName("picture");
        builder.Property(x => x.PostCode).HasColumnName("post_code");
        builder.Property(x => x.County).HasColumnName("county");
        builder.Property(x => x.AutomaticExtTexts).HasColumnName("automatic_ext_texts");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.TaxGroupCode).HasColumnName("tax_group_code");
        builder.Property(x => x.VatProdPostingGroup).HasColumnName("vat_prod_posting_group");
        builder.Property(x => x.CountryRegionCode).HasColumnName("country_region_code");
        builder.Property(x => x.IcPartnerPurchGLAccNo).HasColumnName("ic_partner_purch_g_l_acc_no");
        builder.Property(x => x.Image).HasColumnName("image");
        builder.Property(x => x.UseTimeSheet).HasColumnName("use_time_sheet");
        builder.Property(x => x.TimeSheetOwnerUserId).HasColumnName("time_sheet_owner_user_id");
        builder.Property(x => x.TimeSheetApproverUserId).HasColumnName("time_sheet_approver_user_id");
        builder.Property(x => x.DefaultDeferralTemplateCode).HasColumnName("default_deferral_template_code");
        builder.Property(x => x.ServiceZoneFilter).HasColumnName("service_zone_filter");
    }
}
