using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class GLAccountCategoryConfiguration : IEntityTypeConfiguration<GLAccountCategory>
{
    public void Configure(EntityTypeBuilder<GLAccountCategory> builder)
    {
        builder.ToTable("gl_account_category", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.ParentEntryNo).HasColumnName("parent_entry_no");
        builder.Property(x => x.SiblingSequenceNo).HasColumnName("sibling_sequence_no");
        builder.Property(x => x.PresentationOrder).HasColumnName("presentation_order");
        builder.Property(x => x.Indentation).HasColumnName("indentation");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.AccountCategory).HasColumnName("account_category");
        builder.Property(x => x.IncomeBalance).HasColumnName("income_balance");
        builder.Property(x => x.AdditionalReportDefinition).HasColumnName("additional_report_definition");
        builder.Property(x => x.SystemGenerated).HasColumnName("system_generated");
    }
}
