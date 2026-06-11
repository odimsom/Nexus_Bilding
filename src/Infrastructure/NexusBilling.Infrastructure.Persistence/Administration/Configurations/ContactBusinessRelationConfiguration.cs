using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ContactBusinessRelationConfiguration : IEntityTypeConfiguration<ContactBusinessRelation>
{
    public void Configure(EntityTypeBuilder<ContactBusinessRelation> builder)
    {
        builder.ToTable("contact_business_relation", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ContactNo).HasColumnName("contact_no");
        builder.Property(x => x.BusinessRelationCode).HasColumnName("business_relation_code");
        builder.Property(x => x.LinkToTable).HasColumnName("link_to_table");
        builder.Property(x => x.No).HasColumnName("no");
    }
}
