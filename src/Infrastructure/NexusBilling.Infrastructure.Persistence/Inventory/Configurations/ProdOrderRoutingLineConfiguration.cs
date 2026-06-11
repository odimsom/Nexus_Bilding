using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ProdOrderRoutingLineConfiguration : IEntityTypeConfiguration<ProdOrderRoutingLine>
{
    public void Configure(EntityTypeBuilder<ProdOrderRoutingLine> builder)
    {
        builder.ToTable("prod_order_routing_line", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.RoutingNo).HasColumnName("routing_no");
        builder.Property(x => x.RoutingReferenceNo).HasColumnName("routing_reference_no");
        builder.Property(x => x.OperationNo).HasColumnName("operation_no");
        builder.Property(x => x.NextOperationNo).HasColumnName("next_operation_no");
        builder.Property(x => x.PreviousOperationNo).HasColumnName("previous_operation_no");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.WorkCenterNo).HasColumnName("work_center_no");
        builder.Property(x => x.WorkCenterGroupCode).HasColumnName("work_center_group_code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.SetupTime).HasColumnName("setup_time").HasPrecision(18, 5);
        builder.Property(x => x.RunTime).HasColumnName("run_time").HasPrecision(18, 5);
        builder.Property(x => x.WaitTime).HasColumnName("wait_time").HasPrecision(18, 5);
        builder.Property(x => x.MoveTime).HasColumnName("move_time").HasPrecision(18, 5);
        builder.Property(x => x.FixedScrapQuantity).HasColumnName("fixed_scrap_quantity").HasPrecision(18, 5);
        builder.Property(x => x.LotSize).HasColumnName("lot_size").HasPrecision(18, 5);
        builder.Property(x => x.ScrapFactor).HasColumnName("scrap_factor").HasPrecision(18, 5);
        builder.Property(x => x.SetupTimeUnitOfMeasCode).HasColumnName("setup_time_unit_of_meas_code");
        builder.Property(x => x.RunTimeUnitOfMeasCode).HasColumnName("run_time_unit_of_meas_code");
        builder.Property(x => x.WaitTimeUnitOfMeasCode).HasColumnName("wait_time_unit_of_meas_code");
        builder.Property(x => x.MoveTimeUnitOfMeasCode).HasColumnName("move_time_unit_of_meas_code");
        builder.Property(x => x.MinimumProcessTime).HasColumnName("minimum_process_time").HasPrecision(18, 5);
        builder.Property(x => x.MaximumProcessTime).HasColumnName("maximum_process_time").HasPrecision(18, 5);
        builder.Property(x => x.ConcurrentCapacities).HasColumnName("concurrent_capacities").HasPrecision(18, 5);
        builder.Property(x => x.SendAheadQuantity).HasColumnName("send_ahead_quantity").HasPrecision(18, 5);
        builder.Property(x => x.RoutingLinkCode).HasColumnName("routing_link_code");
        builder.Property(x => x.StandardTaskCode).HasColumnName("standard_task_code");
        builder.Property(x => x.UnitCostPer).HasColumnName("unit_cost_per").HasPrecision(18, 5);
        builder.Property(x => x.Recalculate).HasColumnName("recalculate");
        builder.Property(x => x.SequenceNoForward).HasColumnName("sequence_no_forward");
        builder.Property(x => x.SequenceNoBackward).HasColumnName("sequence_no_backward");
        builder.Property(x => x.FixedScrapQtyAccum).HasColumnName("fixed_scrap_qty_accum").HasPrecision(18, 5);
        builder.Property(x => x.ScrapFactorAccumulated).HasColumnName("scrap_factor_accumulated").HasPrecision(18, 5);
        builder.Property(x => x.SequenceNoActual).HasColumnName("sequence_no_actual");
        builder.Property(x => x.DirectUnitCost).HasColumnName("direct_unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.IndirectCost).HasColumnName("indirect_cost").HasPrecision(18, 5);
        builder.Property(x => x.OverheadRate).HasColumnName("overhead_rate").HasPrecision(18, 5);
        builder.Property(x => x.StartingTime).HasColumnName("starting_time");
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
        builder.Property(x => x.EndingTime).HasColumnName("ending_time");
        builder.Property(x => x.EndingDate).HasColumnName("ending_date");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.ProdOrderNo).HasColumnName("prod_order_no");
        builder.Property(x => x.UnitCostCalculation).HasColumnName("unit_cost_calculation");
        builder.Property(x => x.InputQuantity).HasColumnName("input_quantity").HasPrecision(18, 5);
        builder.Property(x => x.CriticalPath).HasColumnName("critical_path");
        builder.Property(x => x.RoutingStatus).HasColumnName("routing_status");
        builder.Property(x => x.FlushingMethod).HasColumnName("flushing_method");
        builder.Property(x => x.ExpectedOperationCostAmt).HasColumnName("expected_operation_cost_amt").HasPrecision(18, 5);
        builder.Property(x => x.ExpectedCapacityNeed).HasColumnName("expected_capacity_need").HasPrecision(18, 5);
        builder.Property(x => x.ExpectedCapacityOvhdCost).HasColumnName("expected_capacity_ovhd_cost").HasPrecision(18, 5);
        builder.Property(x => x.StartingDateTime).HasColumnName("starting_date_time");
        builder.Property(x => x.EndingDateTime).HasColumnName("ending_date_time");
        builder.Property(x => x.ScheduleManually).HasColumnName("schedule_manually");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.OpenShopFloorBinCode).HasColumnName("open_shop_floor_bin_code");
        builder.Property(x => x.ToProductionBinCode).HasColumnName("to_production_bin_code");
        builder.Property(x => x.FromProductionBinCode).HasColumnName("from_production_bin_code");
    }
}
