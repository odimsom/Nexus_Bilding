using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class NoSeriesLineConfiguration : IEntityTypeConfiguration<NoSeriesLine>
{
    public void Configure(EntityTypeBuilder<NoSeriesLine> builder)
    {
        builder.ToTable("no_series_line", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.SeriesCode).HasColumnName("series_code");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
        builder.Property(x => x.StartingNo).HasColumnName("starting_no");
        builder.Property(x => x.EndingNo).HasColumnName("ending_no");
        builder.Property(x => x.WarningNo).HasColumnName("warning_no");
        builder.Property(x => x.IncrementByNo).HasColumnName("increment_by_no");
        builder.Property(x => x.LastNoUsed).HasColumnName("last_no_used");
        builder.Property(x => x.Open).HasColumnName("open");
        builder.Property(x => x.LastDateUsed).HasColumnName("last_date_used");
    }
}
