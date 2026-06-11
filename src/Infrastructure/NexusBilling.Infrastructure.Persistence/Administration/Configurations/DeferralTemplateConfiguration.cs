using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class DeferralTemplateConfiguration : IEntityTypeConfiguration<DeferralTemplate>
{
    public void Configure(EntityTypeBuilder<DeferralTemplate> builder)
    {
        builder.ToTable("deferral_template", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.DeferralCode).HasColumnName("deferral_code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.DeferralAccount).HasColumnName("deferral_account");
        builder.Property(x => x.Deferral).HasColumnName("deferral").HasPrecision(18, 5);
        builder.Property(x => x.CalcMethod).HasColumnName("calc_method");
        builder.Property(x => x.StartDate).HasColumnName("start_date");
        builder.Property(x => x.NoOfPeriods).HasColumnName("no_of_periods");
        builder.Property(x => x.PeriodDescription).HasColumnName("period_description");
    }
}
