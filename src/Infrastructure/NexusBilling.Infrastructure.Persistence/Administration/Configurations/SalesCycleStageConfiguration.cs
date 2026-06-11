using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class SalesCycleStageConfiguration : IEntityTypeConfiguration<SalesCycleStage>
{
    public void Configure(EntityTypeBuilder<SalesCycleStage> builder)
    {
        builder.ToTable("sales_cycle_stage", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.SalesCycleCode).HasColumnName("sales_cycle_code");
        builder.Property(x => x.Stage).HasColumnName("stage");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Completed).HasColumnName("completed").HasPrecision(18, 5);
        builder.Property(x => x.ActivityCode).HasColumnName("activity_code");
        builder.Property(x => x.QuoteRequired).HasColumnName("quote_required");
        builder.Property(x => x.AllowSkip).HasColumnName("allow_skip");
        builder.Property(x => x.DateFormula).HasColumnName("date_formula");
    }
}
