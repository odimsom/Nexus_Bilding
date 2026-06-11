using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AddInConfiguration : IEntityTypeConfiguration<AddIn>
{
    public void Configure(EntityTypeBuilder<AddIn> builder)
    {
        builder.ToTable("add_in", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.AddInName).HasColumnName("add_in_name");
        builder.Property(x => x.PublicKeyToken).HasColumnName("public_key_token");
        builder.Property(x => x.Version).HasColumnName("version");
        builder.Property(x => x.Category).HasColumnName("category");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Resource).HasColumnName("resource");
    }
}
