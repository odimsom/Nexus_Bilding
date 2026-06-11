using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class PostCodeConfiguration : IEntityTypeConfiguration<PostCode>
{
    public void Configure(EntityTypeBuilder<PostCode> builder)
    {
        builder.ToTable("post_code", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.City).HasColumnName("city");
        builder.Property(x => x.SearchCity).HasColumnName("search_city");
        builder.Property(x => x.CountryRegionCode).HasColumnName("country_region_code");
        builder.Property(x => x.County).HasColumnName("county");
    }
}
