using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AnalysisViewConfiguration : IEntityTypeConfiguration<AnalysisView>
{
    public void Configure(EntityTypeBuilder<AnalysisView> builder)
    {
        builder.ToTable("analysis_view", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.AccountSource).HasColumnName("account_source");
        builder.Property(x => x.LastEntryNo).HasColumnName("last_entry_no");
        builder.Property(x => x.LastBudgetEntryNo).HasColumnName("last_budget_entry_no");
        builder.Property(x => x.LastDateUpdated).HasColumnName("last_date_updated");
        builder.Property(x => x.UpdateOnPosting).HasColumnName("update_on_posting");
        builder.Property(x => x.Blocked).HasColumnName("blocked");
        builder.Property(x => x.AccountFilter).HasColumnName("account_filter");
        builder.Property(x => x.BusinessUnitFilter).HasColumnName("business_unit_filter");
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
        builder.Property(x => x.DateCompression).HasColumnName("date_compression");
        builder.Property(x => x.Dimension1Code).HasColumnName("dimension_1_code");
        builder.Property(x => x.Dimension2Code).HasColumnName("dimension_2_code");
        builder.Property(x => x.Dimension3Code).HasColumnName("dimension_3_code");
        builder.Property(x => x.Dimension4Code).HasColumnName("dimension_4_code");
        builder.Property(x => x.IncludeBudgets).HasColumnName("include_budgets");
        builder.Property(x => x.RefreshWhenUnblocked).HasColumnName("refresh_when_unblocked");
    }
}
