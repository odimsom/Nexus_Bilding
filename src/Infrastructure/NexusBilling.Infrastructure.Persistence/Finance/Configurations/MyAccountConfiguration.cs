using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class MyAccountConfiguration : IEntityTypeConfiguration<MyAccount>
{
    public void Configure(EntityTypeBuilder<MyAccount> builder)
    {
        builder.ToTable("my_account", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.AccountNo).HasColumnName("account_no");
        builder.Property(x => x.Name).HasColumnName("name");
    }
}
