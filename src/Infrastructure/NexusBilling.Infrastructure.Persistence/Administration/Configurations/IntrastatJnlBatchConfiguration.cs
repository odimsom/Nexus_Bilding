using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class IntrastatJnlBatchConfiguration : IEntityTypeConfiguration<IntrastatJnlBatch>
{
    public void Configure(EntityTypeBuilder<IntrastatJnlBatch> builder)
    {
        builder.ToTable("intrastat_jnl_batch", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.JournalTemplateName).HasColumnName("journal_template_name");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Reported).HasColumnName("reported");
        builder.Property(x => x.StatisticsPeriod).HasColumnName("statistics_period");
        builder.Property(x => x.AmountsInAddCurrency).HasColumnName("amounts_in_add_currency");
        builder.Property(x => x.CurrencyIdentifier).HasColumnName("currency_identifier");
    }
}
