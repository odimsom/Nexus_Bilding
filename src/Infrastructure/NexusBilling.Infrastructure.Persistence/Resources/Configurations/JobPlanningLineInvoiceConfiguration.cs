using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class JobPlanningLineInvoiceConfiguration : IEntityTypeConfiguration<JobPlanningLineInvoice>
{
    public void Configure(EntityTypeBuilder<JobPlanningLineInvoice> builder)
    {
        builder.ToTable("job_planning_line_invoice", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.JobNo).HasColumnName("job_no");
        builder.Property(x => x.JobTaskNo).HasColumnName("job_task_no");
        builder.Property(x => x.JobPlanningLineNo).HasColumnName("job_planning_line_no");
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.QuantityTransferred).HasColumnName("quantity_transferred").HasPrecision(18, 5);
        builder.Property(x => x.TransferredDate).HasColumnName("transferred_date");
        builder.Property(x => x.InvoicedDate).HasColumnName("invoiced_date");
        builder.Property(x => x.InvoicedAmountLcy).HasColumnName("invoiced_amount_lcy").HasPrecision(18, 5);
        builder.Property(x => x.InvoicedCostAmountLcy).HasColumnName("invoiced_cost_amount_lcy").HasPrecision(18, 5);
        builder.Property(x => x.JobLedgerEntryNo).HasColumnName("job_ledger_entry_no");
    }
}
