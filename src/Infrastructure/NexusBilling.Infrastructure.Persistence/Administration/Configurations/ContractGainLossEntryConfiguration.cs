using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ContractGainLossEntryConfiguration : IEntityTypeConfiguration<ContractGainLossEntry>
{
    public void Configure(EntityTypeBuilder<ContractGainLossEntry> builder)
    {
        builder.ToTable("contract_gain_loss_entry", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.ContractNo).HasColumnName("contract_no");
        builder.Property(x => x.ContractGroupCode).HasColumnName("contract_group_code");
        builder.Property(x => x.ChangeDate).HasColumnName("change_date");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.TypeOfChange).HasColumnName("type_of_change");
        builder.Property(x => x.ResponsibilityCenter).HasColumnName("responsibility_center");
        builder.Property(x => x.CustomerNo).HasColumnName("customer_no");
        builder.Property(x => x.ShipToCode).HasColumnName("ship_to_code");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 5);
    }
}
