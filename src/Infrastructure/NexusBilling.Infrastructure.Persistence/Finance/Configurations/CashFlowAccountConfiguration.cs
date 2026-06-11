using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class CashFlowAccountConfiguration : IEntityTypeConfiguration<CashFlowAccount>
{
    public void Configure(EntityTypeBuilder<CashFlowAccount> builder)
    {
        builder.ToTable("cash_flow_account", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.SearchName).HasColumnName("search_name");
        builder.Property(x => x.AccountType).HasColumnName("account_type");
        builder.Property(x => x.Blocked).HasColumnName("blocked");
        builder.Property(x => x.NewPage).HasColumnName("new_page");
        builder.Property(x => x.NoOfBlankLines).HasColumnName("no_of_blank_lines");
        builder.Property(x => x.Indentation).HasColumnName("indentation");
        builder.Property(x => x.LastDateModified).HasColumnName("last_date_modified");
        builder.Property(x => x.Totaling).HasColumnName("totaling");
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.GLIntegration).HasColumnName("g_l_integration");
        builder.Property(x => x.GLAccountFilter).HasColumnName("g_l_account_filter");
    }
}
