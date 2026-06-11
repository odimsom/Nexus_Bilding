using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class CountryRegionConfiguration : IEntityTypeConfiguration<CountryRegion>
{
    public void Configure(EntityTypeBuilder<CountryRegion> builder)
    {
        builder.ToTable("country_region", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.EuCountryRegionCode).HasColumnName("eu_country_region_code");
        builder.Property(x => x.IntrastatCode).HasColumnName("intrastat_code");
        builder.Property(x => x.AddressFormat).HasColumnName("address_format");
        builder.Property(x => x.ContactAddressFormat).HasColumnName("contact_address_format");
        builder.Property(x => x.VatScheme).HasColumnName("vat_scheme");
    }
}
