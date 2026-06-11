using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ServiceContractAccountGroupConfiguration : IEntityTypeConfiguration<ServiceContractAccountGroup>
{
    public void Configure(EntityTypeBuilder<ServiceContractAccountGroup> builder)
    {
        builder.ToTable("service_contract_account_group", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.NonPrepaidContractAcc).HasColumnName("non_prepaid_contract_acc");
        builder.Property(x => x.PrepaidContractAcc).HasColumnName("prepaid_contract_acc");
    }
}
