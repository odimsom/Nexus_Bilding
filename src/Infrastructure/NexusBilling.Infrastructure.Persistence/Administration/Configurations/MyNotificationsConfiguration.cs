using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class MyNotificationsConfiguration : IEntityTypeConfiguration<MyNotifications>
{
    public void Configure(EntityTypeBuilder<MyNotifications> builder)
    {
        builder.ToTable("my_notifications", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.NotificationId).HasColumnName("notification_id");
        builder.Property(x => x.ApplyToTableId).HasColumnName("apply_to_table_id");
        builder.Property(x => x.Enabled).HasColumnName("enabled");
        builder.Property(x => x.ApplyToTableFilter).HasColumnName("apply_to_table_filter");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
    }
}
