using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ContactIndustryGroupConfiguration : IEntityTypeConfiguration<ContactIndustryGroup>
{
    public void Configure(EntityTypeBuilder<ContactIndustryGroup> builder)
    {
        builder.ToTable("contact_industry_group", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ContactNo).HasColumnName("contact_no");
        builder.Property(x => x.IndustryGroupCode).HasColumnName("industry_group_code");
    }
}
