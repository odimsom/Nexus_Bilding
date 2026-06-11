using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class WarrantyLedgerEntryConfiguration : IEntityTypeConfiguration<WarrantyLedgerEntry>
{
    public void Configure(EntityTypeBuilder<WarrantyLedgerEntry> builder)
    {
        builder.ToTable("warranty_ledger_entry", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.CustomerNo).HasColumnName("customer_no");
        builder.Property(x => x.ShipToCode).HasColumnName("ship_to_code");
        builder.Property(x => x.BillToCustomerNo).HasColumnName("bill_to_customer_no");
        builder.Property(x => x.VariantCodeServiced).HasColumnName("variant_code_serviced");
        builder.Property(x => x.ServiceItemNoServiced).HasColumnName("service_item_no_serviced");
        builder.Property(x => x.ItemNoServiced).HasColumnName("item_no_serviced");
        builder.Property(x => x.SerialNoServiced).HasColumnName("serial_no_serviced");
        builder.Property(x => x.ServiceItemGroupServiced).HasColumnName("service_item_group_serviced");
        builder.Property(x => x.ServiceOrderNo).HasColumnName("service_order_no");
        builder.Property(x => x.ServiceContractNo).HasColumnName("service_contract_no");
        builder.Property(x => x.FaultReasonCode).HasColumnName("fault_reason_code");
        builder.Property(x => x.FaultAreaCode).HasColumnName("fault_area_code");
        builder.Property(x => x.FaultCode).HasColumnName("fault_code");
        builder.Property(x => x.SymptomCode).HasColumnName("symptom_code");
        builder.Property(x => x.ResolutionCode).HasColumnName("resolution_code");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.WorkTypeCode).HasColumnName("work_type_code");
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 5);
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.GenBusPostingGroup).HasColumnName("gen_bus_posting_group");
        builder.Property(x => x.GenProdPostingGroup).HasColumnName("gen_prod_posting_group");
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension_1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension_2_code");
        builder.Property(x => x.Open).HasColumnName("open");
        builder.Property(x => x.VendorNo).HasColumnName("vendor_no");
        builder.Property(x => x.VendorItemNo).HasColumnName("vendor_item_no");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.ServiceOrderLineNo).HasColumnName("service_order_line_no");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
    }
}
