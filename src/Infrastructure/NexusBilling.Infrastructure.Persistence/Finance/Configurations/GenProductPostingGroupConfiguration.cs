using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class GenProductPostingGroupConfiguration : IEntityTypeConfiguration<GenProductPostingGroup>
{
    public void Configure(EntityTypeBuilder<GenProductPostingGroup> builder)
    {
        builder.ToTable("gen_product_posting_group", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.DefVatProdPostingGroup).HasColumnName("def_vat_prod_posting_group");
        builder.Property(x => x.AutoInsertDefault).HasColumnName("auto_insert_default");
    }
}
