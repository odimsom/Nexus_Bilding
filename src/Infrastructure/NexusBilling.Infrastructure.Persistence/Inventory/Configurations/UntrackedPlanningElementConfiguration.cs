using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class UntrackedPlanningElementConfiguration : IEntityTypeConfiguration<UntrackedPlanningElement>
{
    public void Configure(EntityTypeBuilder<UntrackedPlanningElement> builder)
    {
        builder.ToTable("untracked_planning_element", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.WorksheetTemplateName).HasColumnName("worksheet_template_name");
        builder.Property(x => x.WorksheetBatchName).HasColumnName("worksheet_batch_name");
        builder.Property(x => x.WorksheetLineNo).HasColumnName("worksheet_line_no");
        builder.Property(x => x.TrackLineNo).HasColumnName("track_line_no");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.SourceId).HasColumnName("source_id");
        builder.Property(x => x.ParameterValue).HasColumnName("parameter_value").HasPrecision(18, 5);
        builder.Property(x => x.UntrackedQuantity).HasColumnName("untracked_quantity").HasPrecision(18, 5);
        builder.Property(x => x.TrackQuantityFrom).HasColumnName("track_quantity_from").HasPrecision(18, 5);
        builder.Property(x => x.TrackQuantityTo).HasColumnName("track_quantity_to").HasPrecision(18, 5);
        builder.Property(x => x.Source).HasColumnName("source");
        builder.Property(x => x.WarningLevel).HasColumnName("warning_level");
    }
}
