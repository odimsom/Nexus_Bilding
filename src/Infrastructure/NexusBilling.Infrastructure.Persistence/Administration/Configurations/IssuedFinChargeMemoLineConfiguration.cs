using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class IssuedFinChargeMemoLineConfiguration : IEntityTypeConfiguration<IssuedFinChargeMemoLine>
{
    public void Configure(EntityTypeBuilder<IssuedFinChargeMemoLine> builder)
    {
        builder.ToTable("issued_fin_charge_memo_line", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.FinanceChargeMemoNo).HasColumnName("finance_charge_memo_no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.AttachedToLineNo).HasColumnName("attached_to_line_no");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.DocumentDate).HasColumnName("document_date");
        builder.Property(x => x.DueDate).HasColumnName("due_date");
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.OriginalAmount).HasColumnName("original_amount").HasPrecision(18, 5);
        builder.Property(x => x.RemainingAmount).HasColumnName("remaining_amount").HasPrecision(18, 5);
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 5);
        builder.Property(x => x.InterestRate).HasColumnName("interest_rate").HasPrecision(18, 5);
        builder.Property(x => x.GenProdPostingGroup).HasColumnName("gen_prod_posting_group");
        builder.Property(x => x.Vat).HasColumnName("vat").HasPrecision(18, 5);
        builder.Property(x => x.VatCalculationType).HasColumnName("vat_calculation_type");
        builder.Property(x => x.VatAmount).HasColumnName("vat_amount").HasPrecision(18, 5);
        builder.Property(x => x.TaxGroupCode).HasColumnName("tax_group_code");
        builder.Property(x => x.VatProdPostingGroup).HasColumnName("vat_prod_posting_group");
        builder.Property(x => x.VatIdentifier).HasColumnName("vat_identifier");
        builder.Property(x => x.VatClauseCode).HasColumnName("vat_clause_code");
        builder.Property(x => x.SystemCreatedEntry).HasColumnName("system_created_entry");
    }
}
