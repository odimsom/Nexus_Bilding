using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class IcOutboxTransactionConfiguration : IEntityTypeConfiguration<IcOutboxTransaction>
{
    public void Configure(EntityTypeBuilder<IcOutboxTransaction> builder)
    {
        builder.ToTable("ic_outbox_transaction", "purchasing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.TransactionNo).HasColumnName("transaction_no");
        builder.Property(x => x.IcPartnerCode).HasColumnName("ic_partner_code");
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.TransactionSource).HasColumnName("transaction_source");
        builder.Property(x => x.DocumentDate).HasColumnName("document_date");
        builder.Property(x => x.LineAction).HasColumnName("line_action");
        builder.Property(x => x.IcPartnerGLAccNo).HasColumnName("ic_partner_g_l_acc_no");
        builder.Property(x => x.SourceLineNo).HasColumnName("source_line_no");
    }
}
