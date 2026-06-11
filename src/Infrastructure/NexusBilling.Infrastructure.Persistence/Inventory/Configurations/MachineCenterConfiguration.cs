using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class MachineCenterConfiguration : IEntityTypeConfiguration<MachineCenter>
{
    public void Configure(EntityTypeBuilder<MachineCenter> builder)
    {
        builder.ToTable("machine_center", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.SearchName).HasColumnName("search_name");
        builder.Property(x => x.Name2).HasColumnName("name_2");
        builder.Property(x => x.Address).HasColumnName("address");
        builder.Property(x => x.Address2).HasColumnName("address_2");
        builder.Property(x => x.City).HasColumnName("city");
        builder.Property(x => x.PostCode).HasColumnName("post_code");
        builder.Property(x => x.WorkCenterNo).HasColumnName("work_center_no");
        builder.Property(x => x.DirectUnitCost).HasColumnName("direct_unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.IndirectCost).HasColumnName("indirect_cost").HasPrecision(18, 5);
        builder.Property(x => x.UnitCost).HasColumnName("unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.QueueTime).HasColumnName("queue_time").HasPrecision(18, 5);
        builder.Property(x => x.QueueTimeUnitOfMeasCode).HasColumnName("queue_time_unit_of_meas_code");
        builder.Property(x => x.LastDateModified).HasColumnName("last_date_modified");
        builder.Property(x => x.Capacity).HasColumnName("capacity").HasPrecision(18, 5);
        builder.Property(x => x.Efficiency).HasColumnName("efficiency").HasPrecision(18, 5);
        builder.Property(x => x.MaximumEfficiency).HasColumnName("maximum_efficiency").HasPrecision(18, 5);
        builder.Property(x => x.MinimumEfficiency).HasColumnName("minimum_efficiency").HasPrecision(18, 5);
        builder.Property(x => x.Blocked).HasColumnName("blocked");
        builder.Property(x => x.SetupTime).HasColumnName("setup_time").HasPrecision(18, 5);
        builder.Property(x => x.WaitTime).HasColumnName("wait_time").HasPrecision(18, 5);
        builder.Property(x => x.MoveTime).HasColumnName("move_time").HasPrecision(18, 5);
        builder.Property(x => x.FixedScrapQuantity).HasColumnName("fixed_scrap_quantity").HasPrecision(18, 5);
        builder.Property(x => x.Scrap).HasColumnName("scrap").HasPrecision(18, 5);
        builder.Property(x => x.SetupTimeUnitOfMeasCode).HasColumnName("setup_time_unit_of_meas_code");
        builder.Property(x => x.WaitTimeUnitOfMeasCode).HasColumnName("wait_time_unit_of_meas_code");
        builder.Property(x => x.SendAheadQuantity).HasColumnName("send_ahead_quantity").HasPrecision(18, 5);
        builder.Property(x => x.MoveTimeUnitOfMeasCode).HasColumnName("move_time_unit_of_meas_code");
        builder.Property(x => x.FlushingMethod).HasColumnName("flushing_method");
        builder.Property(x => x.MinimumProcessTime).HasColumnName("minimum_process_time").HasPrecision(18, 5);
        builder.Property(x => x.MaximumProcessTime).HasColumnName("maximum_process_time").HasPrecision(18, 5);
        builder.Property(x => x.ConcurrentCapacities).HasColumnName("concurrent_capacities").HasPrecision(18, 5);
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.OverheadRate).HasColumnName("overhead_rate").HasPrecision(18, 5);
        builder.Property(x => x.GenProdPostingGroup).HasColumnName("gen_prod_posting_group");
        builder.Property(x => x.County).HasColumnName("county");
        builder.Property(x => x.CountryRegionCode).HasColumnName("country_region_code");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.OpenShopFloorBinCode).HasColumnName("open_shop_floor_bin_code");
        builder.Property(x => x.ToProductionBinCode).HasColumnName("to_production_bin_code");
        builder.Property(x => x.FromProductionBinCode).HasColumnName("from_production_bin_code");
    }
}
