using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ContractChangeLogConfiguration : IEntityTypeConfiguration<ContractChangeLog>
{
    public void Configure(EntityTypeBuilder<ContractChangeLog> builder)
    {
        builder.ToTable("contract_change_log", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ContractType).HasColumnName("contract_type");
        builder.Property(x => x.ContractNo).HasColumnName("contract_no");
        builder.Property(x => x.ChangeNo).HasColumnName("change_no");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.DateOfChange).HasColumnName("date_of_change");
        builder.Property(x => x.TimeOfChange).HasColumnName("time_of_change");
        builder.Property(x => x.ContractPart).HasColumnName("contract_part");
        builder.Property(x => x.FieldDescription).HasColumnName("field_description");
        builder.Property(x => x.OldValue).HasColumnName("old_value");
        builder.Property(x => x.NewValue).HasColumnName("new_value");
        builder.Property(x => x.TypeOfChange).HasColumnName("type_of_change");
        builder.Property(x => x.ServiceItemNo).HasColumnName("service_item_no");
        builder.Property(x => x.ServContractLineNo).HasColumnName("serv_contract_line_no");
    }
}
