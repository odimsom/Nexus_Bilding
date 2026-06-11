using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class WebhookSubscriptionConfiguration : IEntityTypeConfiguration<WebhookSubscription>
{
    public void Configure(EntityTypeBuilder<WebhookSubscription> builder)
    {
        builder.ToTable("webhook_subscription", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.SubscriptionId).HasColumnName("subscription_id");
        builder.Property(x => x.Endpoint).HasColumnName("endpoint");
        builder.Property(x => x.ClientState).HasColumnName("client_state");
        builder.Property(x => x.CreatedBy).HasColumnName("created_by");
        builder.Property(x => x.RunNotificationAs).HasColumnName("run_notification_as");
        builder.Property(x => x.CompanyName).HasColumnName("company_name");
    }
}
