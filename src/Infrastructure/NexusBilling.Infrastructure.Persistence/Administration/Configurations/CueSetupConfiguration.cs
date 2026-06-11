using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class CueSetupConfiguration : IEntityTypeConfiguration<CueSetup>
{
    public void Configure(EntityTypeBuilder<CueSetup> builder)
    {
        builder.ToTable("cue_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserName).HasColumnName("user_name");
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.FieldNo).HasColumnName("field_no");
        builder.Property(x => x.LowRangeStyle).HasColumnName("low_range_style");
        builder.Property(x => x.Threshold1).HasColumnName("threshold_1").HasPrecision(18, 5);
        builder.Property(x => x.MiddleRangeStyle).HasColumnName("middle_range_style");
        builder.Property(x => x.Threshold2).HasColumnName("threshold_2").HasPrecision(18, 5);
        builder.Property(x => x.HighRangeStyle).HasColumnName("high_range_style");
        builder.Property(x => x.Personalized).HasColumnName("personalized");
    }
}
