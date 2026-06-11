using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ManufacturingSetupConfiguration : IEntityTypeConfiguration<ManufacturingSetup>
{
    public void Configure(EntityTypeBuilder<ManufacturingSetup> builder)
    {
        builder.ToTable("manufacturing_setup", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.NormalStartingTime).HasColumnName("normal_starting_time");
        builder.Property(x => x.NormalEndingTime).HasColumnName("normal_ending_time");
        builder.Property(x => x.DocNoIsProdOrderNo).HasColumnName("doc_no_is_prod_order_no");
        builder.Property(x => x.CostInclSetup).HasColumnName("cost_incl_setup");
        builder.Property(x => x.DynamicLowLevelCode).HasColumnName("dynamic_low_level_code");
        builder.Property(x => x.PlanningWarning).HasColumnName("planning_warning");
        builder.Property(x => x.SimulatedOrderNos).HasColumnName("simulated_order_nos");
        builder.Property(x => x.PlannedOrderNos).HasColumnName("planned_order_nos");
        builder.Property(x => x.FirmPlannedOrderNos).HasColumnName("firm_planned_order_nos");
        builder.Property(x => x.ReleasedOrderNos).HasColumnName("released_order_nos");
        builder.Property(x => x.WorkCenterNos).HasColumnName("work_center_nos");
        builder.Property(x => x.MachineCenterNos).HasColumnName("machine_center_nos");
        builder.Property(x => x.ProductionBomNos).HasColumnName("production_bom_nos");
        builder.Property(x => x.RoutingNos).HasColumnName("routing_nos");
        builder.Property(x => x.CurrentProductionForecast).HasColumnName("current_production_forecast");
        builder.Property(x => x.UseForecastOnLocations).HasColumnName("use_forecast_on_locations");
        builder.Property(x => x.CombinedMpsMrpCalculation).HasColumnName("combined_mps_mrp_calculation");
        builder.Property(x => x.ComponentsAtLocation).HasColumnName("components_at_location");
        builder.Property(x => x.DefaultDampenerPeriod).HasColumnName("default_dampener_period");
        builder.Property(x => x.DefaultDampener).HasColumnName("default_dampener").HasPrecision(18, 5);
        builder.Property(x => x.DefaultSafetyLeadTime).HasColumnName("default_safety_lead_time");
        builder.Property(x => x.BlankOverflowLevel).HasColumnName("blank_overflow_level");
        builder.Property(x => x.ShowCapacityIn).HasColumnName("show_capacity_in");
        builder.Property(x => x.PresetOutputQuantity).HasColumnName("preset_output_quantity");
    }
}
