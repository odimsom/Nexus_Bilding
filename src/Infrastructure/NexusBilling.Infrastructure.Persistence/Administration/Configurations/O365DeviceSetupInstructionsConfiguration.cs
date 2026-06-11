using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class O365DeviceSetupInstructionsConfiguration : IEntityTypeConfiguration<O365DeviceSetupInstructions>
{
    public void Configure(EntityTypeBuilder<O365DeviceSetupInstructions> builder)
    {
        builder.ToTable("o365_device_setup_instructions", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Key).HasColumnName("key");
        builder.Property(x => x.SetupUrl).HasColumnName("setup_url");
        builder.Property(x => x.QrCode).HasColumnName("qr_code");
    }
}
