using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class DuplicateSearchStringSetupConfiguration : IEntityTypeConfiguration<DuplicateSearchStringSetup>
{
    public void Configure(EntityTypeBuilder<DuplicateSearchStringSetup> builder)
    {
        builder.ToTable("duplicate_search_string_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Field).HasColumnName("field");
        builder.Property(x => x.PartOfField).HasColumnName("part_of_field");
        builder.Property(x => x.Length).HasColumnName("length");
    }
}
