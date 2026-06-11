using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class TroubleshootingSetupConfiguration : IEntityTypeConfiguration<TroubleshootingSetup>
{
    public void Configure(EntityTypeBuilder<TroubleshootingSetup> builder)
    {
        builder.ToTable("troubleshooting_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.TroubleshootingNo).HasColumnName("troubleshooting_no");
    }
}
