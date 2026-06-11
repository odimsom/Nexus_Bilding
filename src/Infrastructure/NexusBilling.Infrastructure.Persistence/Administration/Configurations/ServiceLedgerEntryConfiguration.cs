using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ServiceLedgerEntryConfiguration : IEntityTypeConfiguration<ServiceLedgerEntry>
{
    public void Configure(EntityTypeBuilder<ServiceLedgerEntry> builder)
    {
        builder.ToTable("service_ledger_entry", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.ServiceContractNo).HasColumnName("service_contract_no");
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.ServContractAccGrCode).HasColumnName("serv_contract_acc_gr_code");
        builder.Property(x => x.DocumentLineNo).HasColumnName("document_line_no");
        builder.Property(x => x.MovedFromPrepaidAcc).HasColumnName("moved_from_prepaid_acc");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.AmountLcy).HasColumnName("amount_lcy").HasPrecision(18, 5);
        builder.Property(x => x.CustomerNo).HasColumnName("customer_no");
        builder.Property(x => x.ShipToCode).HasColumnName("ship_to_code");
        builder.Property(x => x.ItemNoServiced).HasColumnName("item_no_serviced");
        builder.Property(x => x.SerialNoServiced).HasColumnName("serial_no_serviced");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.ContractInvoicePeriod).HasColumnName("contract_invoice_period");
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension_1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension_2_code");
        builder.Property(x => x.ServiceItemNoServiced).HasColumnName("service_item_no_serviced");
        builder.Property(x => x.VariantCodeServiced).HasColumnName("variant_code_serviced");
        builder.Property(x => x.ContractGroupCode).HasColumnName("contract_group_code");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.CostAmount).HasColumnName("cost_amount").HasPrecision(18, 5);
        builder.Property(x => x.DiscountAmount).HasColumnName("discount_amount").HasPrecision(18, 5);
        builder.Property(x => x.UnitCost).HasColumnName("unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.ChargedQty).HasColumnName("charged_qty").HasPrecision(18, 5);
        builder.Property(x => x.UnitPrice).HasColumnName("unit_price").HasPrecision(18, 5);
        builder.Property(x => x.Discount).HasColumnName("discount").HasPrecision(18, 5);
        builder.Property(x => x.ContractDiscAmount).HasColumnName("contract_disc_amount").HasPrecision(18, 5);
        builder.Property(x => x.BillToCustomerNo).HasColumnName("bill_to_customer_no");
        builder.Property(x => x.FaultReasonCode).HasColumnName("fault_reason_code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.ServiceOrderType).HasColumnName("service_order_type");
        builder.Property(x => x.ServiceOrderNo).HasColumnName("service_order_no");
        builder.Property(x => x.JobNo).HasColumnName("job_no");
        builder.Property(x => x.GenBusPostingGroup).HasColumnName("gen_bus_posting_group");
        builder.Property(x => x.GenProdPostingGroup).HasColumnName("gen_prod_posting_group");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.WorkTypeCode).HasColumnName("work_type_code");
        builder.Property(x => x.BinCode).HasColumnName("bin_code");
        builder.Property(x => x.ResponsibilityCenter).HasColumnName("responsibility_center");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.EntryType).HasColumnName("entry_type");
        builder.Property(x => x.Open).HasColumnName("open");
        builder.Property(x => x.ServPriceAdjmtGrCode).HasColumnName("serv_price_adjmt_gr_code");
        builder.Property(x => x.ServicePriceGroupCode).HasColumnName("service_price_group_code");
        builder.Property(x => x.Prepaid).HasColumnName("prepaid");
        builder.Property(x => x.ApplyUntilEntryNo).HasColumnName("apply_until_entry_no");
        builder.Property(x => x.AppliesToEntryNo).HasColumnName("applies_to_entry_no");
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 5);
        builder.Property(x => x.JobTaskNo).HasColumnName("job_task_no");
        builder.Property(x => x.JobLineType).HasColumnName("job_line_type");
        builder.Property(x => x.JobPosted).HasColumnName("job_posted");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
    }
}
