using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class BankAccountPostingGroupConfiguration : IEntityTypeConfiguration<BankAccountPostingGroup>
{
    public void Configure(EntityTypeBuilder<BankAccountPostingGroup> builder)
    {
        builder.ToTable("bank_account_posting_group", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.GLBankAccountNo).HasColumnName("g_l_bank_account_no");
    }
}
