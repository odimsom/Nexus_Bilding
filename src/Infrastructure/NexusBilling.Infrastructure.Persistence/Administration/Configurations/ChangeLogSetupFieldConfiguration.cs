using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ChangeLogSetupFieldConfiguration : IEntityTypeConfiguration<ChangeLogSetupField>
{
    public void Configure(EntityTypeBuilder<ChangeLogSetupField> builder)
    {
        builder.ToTable("change_log_setup_field", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.TableNo).HasColumnName("table_no");
        builder.Property(x => x.FieldNo).HasColumnName("field_no");
        builder.Property(x => x.LogInsertion).HasColumnName("log_insertion");
        builder.Property(x => x.LogModification).HasColumnName("log_modification");
        builder.Property(x => x.LogDeletion).HasColumnName("log_deletion");
    }
}
