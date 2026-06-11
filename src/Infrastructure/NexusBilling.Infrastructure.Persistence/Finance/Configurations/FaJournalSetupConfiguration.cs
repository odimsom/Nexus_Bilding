using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class FaJournalSetupConfiguration : IEntityTypeConfiguration<FaJournalSetup>
{
    public void Configure(EntityTypeBuilder<FaJournalSetup> builder)
    {
        builder.ToTable("fa_journal_setup", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.DepreciationBookCode).HasColumnName("depreciation_book_code");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.FaJnlTemplateName).HasColumnName("fa_jnl_template_name");
        builder.Property(x => x.FaJnlBatchName).HasColumnName("fa_jnl_batch_name");
        builder.Property(x => x.GenJnlTemplateName).HasColumnName("gen_jnl_template_name");
        builder.Property(x => x.GenJnlBatchName).HasColumnName("gen_jnl_batch_name");
        builder.Property(x => x.InsuranceJnlTemplateName).HasColumnName("insurance_jnl_template_name");
        builder.Property(x => x.InsuranceJnlBatchName).HasColumnName("insurance_jnl_batch_name");
    }
}
