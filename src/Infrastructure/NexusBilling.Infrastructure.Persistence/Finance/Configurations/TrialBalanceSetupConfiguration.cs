using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class TrialBalanceSetupConfiguration : IEntityTypeConfiguration<TrialBalanceSetup>
{
    public void Configure(EntityTypeBuilder<TrialBalanceSetup> builder)
    {
        builder.ToTable("trial_balance_setup", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.AccountScheduleName).HasColumnName("account_schedule_name");
        builder.Property(x => x.ColumnLayoutName).HasColumnName("column_layout_name");
    }
}
