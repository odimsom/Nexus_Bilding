using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class InsuranceConfiguration : IEntityTypeConfiguration<Insurance>
{
    public void Configure(EntityTypeBuilder<Insurance> builder)
    {
        builder.ToTable("insurance", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.EffectiveDate).HasColumnName("effective_date");
        builder.Property(x => x.ExpirationDate).HasColumnName("expiration_date");
        builder.Property(x => x.PolicyNo).HasColumnName("policy_no");
        builder.Property(x => x.AnnualPremium).HasColumnName("annual_premium").HasPrecision(18, 5);
        builder.Property(x => x.PolicyCoverage).HasColumnName("policy_coverage").HasPrecision(18, 5);
        builder.Property(x => x.InsuranceType).HasColumnName("insurance_type");
        builder.Property(x => x.LastDateModified).HasColumnName("last_date_modified");
        builder.Property(x => x.InsuranceVendorNo).HasColumnName("insurance_vendor_no");
        builder.Property(x => x.FaClassCode).HasColumnName("fa_class_code");
        builder.Property(x => x.FaSubclassCode).HasColumnName("fa_subclass_code");
        builder.Property(x => x.FaLocationCode).HasColumnName("fa_location_code");
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension_1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension_2_code");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.Blocked).HasColumnName("blocked");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.SearchDescription).HasColumnName("search_description");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
    }
}
