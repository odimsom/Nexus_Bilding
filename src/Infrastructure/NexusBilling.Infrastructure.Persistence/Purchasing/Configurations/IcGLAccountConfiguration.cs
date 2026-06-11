using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class IcGLAccountConfiguration : IEntityTypeConfiguration<IcGLAccount>
{
    public void Configure(EntityTypeBuilder<IcGLAccount> builder)
    {
        builder.ToTable("ic_g_l_account", "purchasing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.AccountType).HasColumnName("account_type");
        builder.Property(x => x.IncomeBalance).HasColumnName("income_balance");
        builder.Property(x => x.Blocked).HasColumnName("blocked");
        builder.Property(x => x.MapToGLAccNo).HasColumnName("map_to_g_l_acc_no");
        builder.Property(x => x.Indentation).HasColumnName("indentation");
    }
}
