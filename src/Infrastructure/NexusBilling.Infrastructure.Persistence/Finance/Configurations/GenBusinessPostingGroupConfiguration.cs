using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class GenBusinessPostingGroupConfiguration : IEntityTypeConfiguration<GenBusinessPostingGroup>
{
    public void Configure(EntityTypeBuilder<GenBusinessPostingGroup> builder)
    {
        builder.ToTable("gen_business_posting_group", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.DefVatBusPostingGroup).HasColumnName("def_vat_bus_posting_group");
        builder.Property(x => x.AutoInsertDefault).HasColumnName("auto_insert_default");
    }
}
