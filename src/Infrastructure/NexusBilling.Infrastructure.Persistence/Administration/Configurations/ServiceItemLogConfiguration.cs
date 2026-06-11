using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ServiceItemLogConfiguration : IEntityTypeConfiguration<ServiceItemLog>
{
    public void Configure(EntityTypeBuilder<ServiceItemLog> builder)
    {
        builder.ToTable("service_item_log", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ServiceItemNo).HasColumnName("service_item_no");
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.EventNo).HasColumnName("event_no");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.After).HasColumnName("after");
        builder.Property(x => x.Before).HasColumnName("before");
        builder.Property(x => x.ChangeDate).HasColumnName("change_date");
        builder.Property(x => x.ChangeTime).HasColumnName("change_time");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
    }
}
