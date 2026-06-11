using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ItemTrackingCodeConfiguration : IEntityTypeConfiguration<ItemTrackingCode>
{
    public void Configure(EntityTypeBuilder<ItemTrackingCode> builder)
    {
        builder.ToTable("item_tracking_code", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.WarrantyDateFormula).HasColumnName("warranty_date_formula");
        builder.Property(x => x.ManWarrantyDateEntryReqd).HasColumnName("man_warranty_date_entry_reqd");
        builder.Property(x => x.ManExpirDateEntryReqd).HasColumnName("man_expir_date_entry_reqd");
        builder.Property(x => x.StrictExpirationPosting).HasColumnName("strict_expiration_posting");
        builder.Property(x => x.SnSpecificTracking).HasColumnName("sn_specific_tracking");
        builder.Property(x => x.SnInfoInboundMustExist).HasColumnName("sn_info_inbound_must_exist");
        builder.Property(x => x.SnInfoOutboundMustExist).HasColumnName("sn_info_outbound_must_exist");
        builder.Property(x => x.SnWarehouseTracking).HasColumnName("sn_warehouse_tracking");
        builder.Property(x => x.SnPurchaseInboundTracking).HasColumnName("sn_purchase_inbound_tracking");
        builder.Property(x => x.SnPurchaseOutboundTracking).HasColumnName("sn_purchase_outbound_tracking");
        builder.Property(x => x.SnSalesInboundTracking).HasColumnName("sn_sales_inbound_tracking");
        builder.Property(x => x.SnSalesOutboundTracking).HasColumnName("sn_sales_outbound_tracking");
        builder.Property(x => x.SnPosAdjmtInbTracking).HasColumnName("sn_pos_adjmt_inb_tracking");
        builder.Property(x => x.SnPosAdjmtOutbTracking).HasColumnName("sn_pos_adjmt_outb_tracking");
        builder.Property(x => x.SnNegAdjmtInbTracking).HasColumnName("sn_neg_adjmt_inb_tracking");
        builder.Property(x => x.SnNegAdjmtOutbTracking).HasColumnName("sn_neg_adjmt_outb_tracking");
        builder.Property(x => x.SnTransferTracking).HasColumnName("sn_transfer_tracking");
        builder.Property(x => x.SnManufInboundTracking).HasColumnName("sn_manuf_inbound_tracking");
        builder.Property(x => x.SnManufOutboundTracking).HasColumnName("sn_manuf_outbound_tracking");
        builder.Property(x => x.SnAssemblyInboundTracking).HasColumnName("sn_assembly_inbound_tracking");
        builder.Property(x => x.SnAssemblyOutboundTracking).HasColumnName("sn_assembly_outbound_tracking");
        builder.Property(x => x.LotSpecificTracking).HasColumnName("lot_specific_tracking");
        builder.Property(x => x.LotInfoInboundMustExist).HasColumnName("lot_info_inbound_must_exist");
        builder.Property(x => x.LotInfoOutboundMustExist).HasColumnName("lot_info_outbound_must_exist");
        builder.Property(x => x.LotWarehouseTracking).HasColumnName("lot_warehouse_tracking");
        builder.Property(x => x.LotPurchaseInboundTracking).HasColumnName("lot_purchase_inbound_tracking");
        builder.Property(x => x.LotPurchaseOutboundTracking).HasColumnName("lot_purchase_outbound_tracking");
        builder.Property(x => x.LotSalesInboundTracking).HasColumnName("lot_sales_inbound_tracking");
        builder.Property(x => x.LotSalesOutboundTracking).HasColumnName("lot_sales_outbound_tracking");
        builder.Property(x => x.LotPosAdjmtInbTracking).HasColumnName("lot_pos_adjmt_inb_tracking");
        builder.Property(x => x.LotPosAdjmtOutbTracking).HasColumnName("lot_pos_adjmt_outb_tracking");
        builder.Property(x => x.LotNegAdjmtInbTracking).HasColumnName("lot_neg_adjmt_inb_tracking");
        builder.Property(x => x.LotNegAdjmtOutbTracking).HasColumnName("lot_neg_adjmt_outb_tracking");
        builder.Property(x => x.LotTransferTracking).HasColumnName("lot_transfer_tracking");
        builder.Property(x => x.LotManufInboundTracking).HasColumnName("lot_manuf_inbound_tracking");
        builder.Property(x => x.LotManufOutboundTracking).HasColumnName("lot_manuf_outbound_tracking");
        builder.Property(x => x.LotAssemblyInboundTracking).HasColumnName("lot_assembly_inbound_tracking");
        builder.Property(x => x.LotAssemblyOutboundTracking).HasColumnName("lot_assembly_outbound_tracking");
    }
}
