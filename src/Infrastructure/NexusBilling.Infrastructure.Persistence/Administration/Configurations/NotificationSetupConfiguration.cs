using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class NotificationSetupConfiguration : IEntityTypeConfiguration<NotificationSetup>
{
    public void Configure(EntityTypeBuilder<NotificationSetup> builder)
    {
        builder.ToTable("notification_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.NotificationType).HasColumnName("notification_type");
        builder.Property(x => x.NotificationMethod).HasColumnName("notification_method");
        builder.Property(x => x.DisplayTarget).HasColumnName("display_target");
    }
}
