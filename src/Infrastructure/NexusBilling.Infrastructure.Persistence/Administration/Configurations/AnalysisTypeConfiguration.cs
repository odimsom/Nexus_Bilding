using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AnalysisTypeConfiguration : IEntityTypeConfiguration<AnalysisType>
{
    public void Configure(EntityTypeBuilder<AnalysisType> builder)
    {
        builder.ToTable("analysis_type", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.ValueType).HasColumnName("value_type");
        builder.Property(x => x.ItemLedgerEntryTypeFilter).HasColumnName("item_ledger_entry_type_filter");
        builder.Property(x => x.ValueEntryTypeFilter).HasColumnName("value_entry_type_filter");
    }
}
