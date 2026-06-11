using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ChangeLogEntryConfiguration : IEntityTypeConfiguration<ChangeLogEntry>
{
    public void Configure(EntityTypeBuilder<ChangeLogEntry> builder)
    {
        builder.ToTable("change_log_entry", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.DateAndTime).HasColumnName("date_and_time");
        builder.Property(x => x.Time).HasColumnName("time");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.TableNo).HasColumnName("table_no");
        builder.Property(x => x.FieldNo).HasColumnName("field_no");
        builder.Property(x => x.TypeOfChange).HasColumnName("type_of_change");
        builder.Property(x => x.OldValue).HasColumnName("old_value");
        builder.Property(x => x.NewValue).HasColumnName("new_value");
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.PrimaryKeyField1No).HasColumnName("primary_key_field_1_no");
        builder.Property(x => x.PrimaryKeyField1Value).HasColumnName("primary_key_field_1_value");
        builder.Property(x => x.PrimaryKeyField2No).HasColumnName("primary_key_field_2_no");
        builder.Property(x => x.PrimaryKeyField2Value).HasColumnName("primary_key_field_2_value");
        builder.Property(x => x.PrimaryKeyField3No).HasColumnName("primary_key_field_3_no");
        builder.Property(x => x.PrimaryKeyField3Value).HasColumnName("primary_key_field_3_value");
        builder.Property(x => x.RecordId).HasColumnName("record_id");
    }
}
