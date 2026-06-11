using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class FaReclassJournalLineConfiguration : IEntityTypeConfiguration<FaReclassJournalLine>
{
    public void Configure(EntityTypeBuilder<FaReclassJournalLine> builder)
    {
        builder.ToTable("fa_reclass_journal_line", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.JournalTemplateName).HasColumnName("journal_template_name");
        builder.Property(x => x.JournalBatchName).HasColumnName("journal_batch_name");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.FaNo).HasColumnName("fa_no");
        builder.Property(x => x.NewFaNo).HasColumnName("new_fa_no");
        builder.Property(x => x.FaPostingDate).HasColumnName("fa_posting_date");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.DepreciationBookCode).HasColumnName("depreciation_book_code");
        builder.Property(x => x.ReclassifyAcqCostAmount).HasColumnName("reclassify_acq_cost_amount").HasPrecision(18, 5);
        builder.Property(x => x.ReclassifyAcqCost).HasColumnName("reclassify_acq_cost").HasPrecision(18, 5);
        builder.Property(x => x.ReclassifyAcquisitionCost).HasColumnName("reclassify_acquisition_cost");
        builder.Property(x => x.ReclassifyDepreciation).HasColumnName("reclassify_depreciation");
        builder.Property(x => x.ReclassifyWriteDown).HasColumnName("reclassify_write_down");
        builder.Property(x => x.ReclassifyAppreciation).HasColumnName("reclassify_appreciation");
        builder.Property(x => x.ReclassifyCustom1).HasColumnName("reclassify_custom_1");
        builder.Property(x => x.ReclassifyCustom2).HasColumnName("reclassify_custom_2");
        builder.Property(x => x.ReclassifySalvageValue).HasColumnName("reclassify_salvage_value");
        builder.Property(x => x.InsertBalAccount).HasColumnName("insert_bal_account");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.CalcDb1DeprAmount).HasColumnName("calc_db1_depr_amount");
    }
}
