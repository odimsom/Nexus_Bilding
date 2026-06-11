using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AssistedSetupLogConfiguration : IEntityTypeConfiguration<AssistedSetupLog>
{
    public void Configure(EntityTypeBuilder<AssistedSetupLog> builder)
    {
        builder.ToTable("assisted_setup_log", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.EnteryNo).HasColumnName("entery_no");
        builder.Property(x => x.DateTime).HasColumnName("date_time");
        builder.Property(x => x.InvokedAction).HasColumnName("invoked_action");
    }
}
