using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ChangeLogSetupTableConfiguration : IEntityTypeConfiguration<ChangeLogSetupTable>
{
    public void Configure(EntityTypeBuilder<ChangeLogSetupTable> builder)
    {
        builder.ToTable("change_log_setup_table", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.TableNo).HasColumnName("table_no");
        builder.Property(x => x.LogInsertion).HasColumnName("log_insertion");
        builder.Property(x => x.LogModification).HasColumnName("log_modification");
        builder.Property(x => x.LogDeletion).HasColumnName("log_deletion");
    }
}
