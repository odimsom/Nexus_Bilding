using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ServiceShipmentItemLineConfiguration : IEntityTypeConfiguration<ServiceShipmentItemLine>
{
    public void Configure(EntityTypeBuilder<ServiceShipmentItemLine> builder)
    {
        builder.ToTable("service_shipment_item_line", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.ServiceItemNo).HasColumnName("service_item_no");
        builder.Property(x => x.ServiceItemGroupCode).HasColumnName("service_item_group_code");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.SerialNo).HasColumnName("serial_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Description2).HasColumnName("description_2");
        builder.Property(x => x.Priority).HasColumnName("priority");
        builder.Property(x => x.ResponseTimeHours).HasColumnName("response_time_hours").HasPrecision(18, 5);
        builder.Property(x => x.ResponseDate).HasColumnName("response_date");
        builder.Property(x => x.ResponseTime).HasColumnName("response_time");
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
        builder.Property(x => x.StartingTime).HasColumnName("starting_time");
        builder.Property(x => x.FinishingDate).HasColumnName("finishing_date");
        builder.Property(x => x.FinishingTime).HasColumnName("finishing_time");
        builder.Property(x => x.ServiceShelfNo).HasColumnName("service_shelf_no");
        builder.Property(x => x.WarrantyStartingDateParts).HasColumnName("warranty_starting_date_parts");
        builder.Property(x => x.WarrantyEndingDateParts).HasColumnName("warranty_ending_date_parts");
        builder.Property(x => x.Warranty).HasColumnName("warranty");
        builder.Property(x => x.WarrantyParts).HasColumnName("warranty_parts").HasPrecision(18, 5);
        builder.Property(x => x.WarrantyLabor).HasColumnName("warranty_labor").HasPrecision(18, 5);
        builder.Property(x => x.WarrantyStartingDateLabor).HasColumnName("warranty_starting_date_labor");
        builder.Property(x => x.WarrantyEndingDateLabor).HasColumnName("warranty_ending_date_labor");
        builder.Property(x => x.ContractNo).HasColumnName("contract_no");
        builder.Property(x => x.LoanerNo).HasColumnName("loaner_no");
        builder.Property(x => x.VendorNo).HasColumnName("vendor_no");
        builder.Property(x => x.VendorItemNo).HasColumnName("vendor_item_no");
        builder.Property(x => x.FaultReasonCode).HasColumnName("fault_reason_code");
        builder.Property(x => x.ServicePriceGroupCode).HasColumnName("service_price_group_code");
        builder.Property(x => x.FaultAreaCode).HasColumnName("fault_area_code");
        builder.Property(x => x.SymptomCode).HasColumnName("symptom_code");
        builder.Property(x => x.FaultCode).HasColumnName("fault_code");
        builder.Property(x => x.ResolutionCode).HasColumnName("resolution_code");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.ActualResponseTimeHours).HasColumnName("actual_response_time_hours").HasPrecision(18, 5);
        builder.Property(x => x.ServicePriceAdjmtGrCode).HasColumnName("service_price_adjmt_gr_code");
        builder.Property(x => x.AdjustmentType).HasColumnName("adjustment_type");
        builder.Property(x => x.BaseAmountToAdjust).HasColumnName("base_amount_to_adjust").HasPrecision(18, 5);
        builder.Property(x => x.ShipToCode).HasColumnName("ship_to_code");
        builder.Property(x => x.CustomerNo).HasColumnName("customer_no");
        builder.Property(x => x.ResponsibilityCenter).HasColumnName("responsibility_center");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
    }
}
