using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class GLAccountConfiguration : IEntityTypeConfiguration<GLAccount>
{
    public void Configure(EntityTypeBuilder<GLAccount> builder)
    {
        builder.ToTable("g_l_account", "erp");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TenantId)
            .HasConversion(v => v.Value, v => TenantIdentifier.Create(v))
            .HasColumnName("tenant_id")
            .IsRequired();

        builder.Property(x => x.No)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.AccountType)
            .IsRequired();

        builder.Property(x => x.IncomeBalance)
            .IsRequired();

        builder.Property(x => x.Blocked)
            .IsRequired();
            
        builder.HasIndex(x => new { x.TenantId, x.No }).IsUnique();
    }
}
