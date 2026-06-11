using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class AccountingPeriodConfiguration : IEntityTypeConfiguration<AccountingPeriod>
{
    public void Configure(EntityTypeBuilder<AccountingPeriod> builder)
    {
        builder.ToTable("accounting_period", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.NewFiscalYear).HasColumnName("new_fiscal_year");
        builder.Property(x => x.Closed).HasColumnName("closed");
        builder.Property(x => x.DateLocked).HasColumnName("date_locked");
        builder.Property(x => x.AverageCostCalcType).HasColumnName("average_cost_calc_type");
        builder.Property(x => x.AverageCostPeriod).HasColumnName("average_cost_period");
    }
}
