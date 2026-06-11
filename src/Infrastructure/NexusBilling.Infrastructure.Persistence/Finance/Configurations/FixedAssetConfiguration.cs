using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class FixedAssetConfiguration : IEntityTypeConfiguration<FixedAsset>
{
    public void Configure(EntityTypeBuilder<FixedAsset> builder)
    {
        builder.ToTable("fixed_asset", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.SearchDescription).HasColumnName("search_description");
        builder.Property(x => x.Description2).HasColumnName("description_2");
        builder.Property(x => x.FaClassCode).HasColumnName("fa_class_code");
        builder.Property(x => x.FaSubclassCode).HasColumnName("fa_subclass_code");
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension_1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension_2_code");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.FaLocationCode).HasColumnName("fa_location_code");
        builder.Property(x => x.VendorNo).HasColumnName("vendor_no");
        builder.Property(x => x.MainAssetComponent).HasColumnName("main_asset_component");
        builder.Property(x => x.ComponentOfMainAsset).HasColumnName("component_of_main_asset");
        builder.Property(x => x.BudgetedAsset).HasColumnName("budgeted_asset");
        builder.Property(x => x.WarrantyDate).HasColumnName("warranty_date");
        builder.Property(x => x.ResponsibleEmployee).HasColumnName("responsible_employee");
        builder.Property(x => x.SerialNo).HasColumnName("serial_no");
        builder.Property(x => x.LastDateModified).HasColumnName("last_date_modified");
        builder.Property(x => x.Blocked).HasColumnName("blocked");
        builder.Property(x => x.Picture).HasColumnName("picture");
        builder.Property(x => x.MaintenanceVendorNo).HasColumnName("maintenance_vendor_no");
        builder.Property(x => x.UnderMaintenance).HasColumnName("under_maintenance");
        builder.Property(x => x.NextServiceDate).HasColumnName("next_service_date");
        builder.Property(x => x.Inactive).HasColumnName("inactive");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.FaPostingGroup).HasColumnName("fa_posting_group");
        builder.Property(x => x.Image).HasColumnName("image");
    }
}
