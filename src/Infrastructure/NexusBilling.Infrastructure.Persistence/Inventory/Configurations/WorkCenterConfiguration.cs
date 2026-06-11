using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class WorkCenterConfiguration : IEntityTypeConfiguration<WorkCenter>
{
    public void Configure(EntityTypeBuilder<WorkCenter> builder)
    {
        builder.ToTable("work_center", "inventory");
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
        builder.Property(x => x.AlternateWorkCenter).HasColumnName("alternate_work_center");
        builder.Property(x => x.WorkCenterGroupCode).HasColumnName("work_center_group_code");
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension_1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension_2_code");
        builder.Property(x => x.SubcontractorNo).HasColumnName("subcontractor_no");
        builder.Property(x => x.DirectUnitCost).HasColumnName("direct_unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.IndirectCost).HasColumnName("indirect_cost").HasPrecision(18, 5);
        builder.Property(x => x.UnitCost).HasColumnName("unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.QueueTime).HasColumnName("queue_time").HasPrecision(18, 5);
        builder.Property(x => x.QueueTimeUnitOfMeasCode).HasColumnName("queue_time_unit_of_meas_code");
        builder.Property(x => x.LastDateModified).HasColumnName("last_date_modified");
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.Capacity).HasColumnName("capacity").HasPrecision(18, 5);
        builder.Property(x => x.Efficiency).HasColumnName("efficiency").HasPrecision(18, 5);
        builder.Property(x => x.MaximumEfficiency).HasColumnName("maximum_efficiency").HasPrecision(18, 5);
        builder.Property(x => x.MinimumEfficiency).HasColumnName("minimum_efficiency").HasPrecision(18, 5);
        builder.Property(x => x.CalendarRoundingPrecision).HasColumnName("calendar_rounding_precision").HasPrecision(18, 5);
        builder.Property(x => x.SimulationType).HasColumnName("simulation_type");
        builder.Property(x => x.ShopCalendarCode).HasColumnName("shop_calendar_code");
        builder.Property(x => x.Blocked).HasColumnName("blocked");
        builder.Property(x => x.UnitCostCalculation).HasColumnName("unit_cost_calculation");
        builder.Property(x => x.SpecificUnitCost).HasColumnName("specific_unit_cost");
        builder.Property(x => x.ConsolidatedCalendar).HasColumnName("consolidated_calendar");
        builder.Property(x => x.FlushingMethod).HasColumnName("flushing_method");
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
