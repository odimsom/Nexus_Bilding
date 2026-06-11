using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class IntrastatJnlLineConfiguration : IEntityTypeConfiguration<IntrastatJnlLine>
{
    public void Configure(EntityTypeBuilder<IntrastatJnlLine> builder)
    {
        builder.ToTable("intrastat_jnl_line", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.JournalTemplateName).HasColumnName("journal_template_name");
        builder.Property(x => x.JournalBatchName).HasColumnName("journal_batch_name");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.Date).HasColumnName("date");
        builder.Property(x => x.TariffNo).HasColumnName("tariff_no");
        builder.Property(x => x.ItemDescription).HasColumnName("item_description");
        builder.Property(x => x.CountryRegionCode).HasColumnName("country_region_code");
        builder.Property(x => x.TransactionType).HasColumnName("transaction_type");
        builder.Property(x => x.TransportMethod).HasColumnName("transport_method");
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.SourceEntryNo).HasColumnName("source_entry_no");
        builder.Property(x => x.NetWeight).HasColumnName("net_weight").HasPrecision(18, 5);
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 5);
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.CostRegulation).HasColumnName("cost_regulation").HasPrecision(18, 5);
        builder.Property(x => x.IndirectCost).HasColumnName("indirect_cost").HasPrecision(18, 5);
        builder.Property(x => x.StatisticalValue).HasColumnName("statistical_value").HasPrecision(18, 5);
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.TotalWeight).HasColumnName("total_weight").HasPrecision(18, 5);
        builder.Property(x => x.SupplementaryUnits).HasColumnName("supplementary_units");
        builder.Property(x => x.InternalRefNo).HasColumnName("internal_ref_no");
        builder.Property(x => x.CountryRegionOfOriginCode).HasColumnName("country_region_of_origin_code");
        builder.Property(x => x.EntryExitPoint).HasColumnName("entry_exit_point");
        builder.Property(x => x.Area).HasColumnName("area");
        builder.Property(x => x.TransactionSpecification).HasColumnName("transaction_specification");
    }
}
