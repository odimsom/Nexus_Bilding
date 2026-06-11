using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ConfigPackageTableConfiguration : IEntityTypeConfiguration<ConfigPackageTable>
{
    public void Configure(EntityTypeBuilder<ConfigPackageTable> builder)
    {
        builder.ToTable("config_package_table", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PackageCode).HasColumnName("package_code");
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.ImportedDateAndTime).HasColumnName("imported_date_and_time");
        builder.Property(x => x.ExportedDateAndTime).HasColumnName("exported_date_and_time");
        builder.Property(x => x.Comments).HasColumnName("comments");
        builder.Property(x => x.CreatedDateAndTime).HasColumnName("created_date_and_time");
        builder.Property(x => x.DataTemplate).HasColumnName("data_template");
        builder.Property(x => x.PackageProcessingOrder).HasColumnName("package_processing_order");
        builder.Property(x => x.PageId).HasColumnName("page_id");
        builder.Property(x => x.ProcessingOrder).HasColumnName("processing_order");
        builder.Property(x => x.ImportedByUserId).HasColumnName("imported_by_user_id");
        builder.Property(x => x.CreatedByUserId).HasColumnName("created_by_user_id");
        builder.Property(x => x.DimensionsAsColumns).HasColumnName("dimensions_as_columns");
        builder.Property(x => x.SkipTableTriggers).HasColumnName("skip_table_triggers");
        builder.Property(x => x.DeleteRecsBeforeProcessing).HasColumnName("delete_recs_before_processing");
        builder.Property(x => x.ProcessingReportId).HasColumnName("processing_report_id");
        builder.Property(x => x.ParentTableId).HasColumnName("parent_table_id");
        builder.Property(x => x.Validated).HasColumnName("validated");
    }
}
