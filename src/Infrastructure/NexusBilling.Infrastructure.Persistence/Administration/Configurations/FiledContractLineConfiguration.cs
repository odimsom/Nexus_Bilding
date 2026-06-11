using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class FiledContractLineConfiguration : IEntityTypeConfiguration<FiledContractLine>
{
    public void Configure(EntityTypeBuilder<FiledContractLine> builder)
    {
        builder.ToTable("filed_contract_line", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ContractType).HasColumnName("contract_type");
        builder.Property(x => x.ContractNo).HasColumnName("contract_no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.ContractStatus).HasColumnName("contract_status");
        builder.Property(x => x.ServiceItemNo).HasColumnName("service_item_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.SerialNo).HasColumnName("serial_no");
        builder.Property(x => x.ServiceItemGroupCode).HasColumnName("service_item_group_code");
        builder.Property(x => x.CustomerNo).HasColumnName("customer_no");
        builder.Property(x => x.ShipToCode).HasColumnName("ship_to_code");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.ResponseTimeHours).HasColumnName("response_time_hours").HasPrecision(18, 5);
        builder.Property(x => x.LastPlannedServiceDate).HasColumnName("last_planned_service_date");
        builder.Property(x => x.NextPlannedServiceDate).HasColumnName("next_planned_service_date");
        builder.Property(x => x.LastServiceDate).HasColumnName("last_service_date");
        builder.Property(x => x.LastPreventiveMaintDate).HasColumnName("last_preventive_maint_date");
        builder.Property(x => x.InvoicedToDate).HasColumnName("invoiced_to_date");
        builder.Property(x => x.CreditMemoDate).HasColumnName("credit_memo_date");
        builder.Property(x => x.ContractExpirationDate).HasColumnName("contract_expiration_date");
        builder.Property(x => x.ServicePeriod).HasColumnName("service_period");
        builder.Property(x => x.LineValue).HasColumnName("line_value").HasPrecision(18, 5);
        builder.Property(x => x.LineDiscount).HasColumnName("line_discount").HasPrecision(18, 5);
        builder.Property(x => x.LineAmount).HasColumnName("line_amount").HasPrecision(18, 5);
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
        builder.Property(x => x.NewLine).HasColumnName("new_line");
        builder.Property(x => x.Credited).HasColumnName("credited");
        builder.Property(x => x.LineCost).HasColumnName("line_cost").HasPrecision(18, 5);
        builder.Property(x => x.LineDiscountAmount).HasColumnName("line_discount_amount").HasPrecision(18, 5);
        builder.Property(x => x.Profit).HasColumnName("profit").HasPrecision(18, 5);
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
    }
}
