using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ResponsibilityCenterConfiguration : IEntityTypeConfiguration<ResponsibilityCenter>
{
    public void Configure(EntityTypeBuilder<ResponsibilityCenter> builder)
    {
        builder.ToTable("responsibility_center", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Address).HasColumnName("address");
        builder.Property(x => x.Address2).HasColumnName("address_2");
        builder.Property(x => x.City).HasColumnName("city");
        builder.Property(x => x.PostCode).HasColumnName("post_code");
        builder.Property(x => x.CountryRegionCode).HasColumnName("country_region_code");
        builder.Property(x => x.PhoneNo).HasColumnName("phone_no");
        builder.Property(x => x.FaxNo).HasColumnName("fax_no");
        builder.Property(x => x.Name2).HasColumnName("name_2");
        builder.Property(x => x.Contact).HasColumnName("contact");
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension_1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension_2_code");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.County).HasColumnName("county");
        builder.Property(x => x.EMail).HasColumnName("e_mail");
        builder.Property(x => x.HomePage).HasColumnName("home_page");
    }
}
