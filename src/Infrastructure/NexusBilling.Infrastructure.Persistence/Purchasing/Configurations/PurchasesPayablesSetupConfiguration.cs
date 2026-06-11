using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class PurchasesPayablesSetupConfiguration : IEntityTypeConfiguration<PurchasesPayablesSetup>
{
    public void Configure(EntityTypeBuilder<PurchasesPayablesSetup> builder)
    {
        builder.ToTable("purchases_payables_setup", "purchasing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.DiscountPosting).HasColumnName("discount_posting");
        builder.Property(x => x.ReceiptOnInvoice).HasColumnName("receipt_on_invoice");
        builder.Property(x => x.InvoiceRounding).HasColumnName("invoice_rounding");
        builder.Property(x => x.ExtDocNoMandatory).HasColumnName("ext_doc_no_mandatory");
        builder.Property(x => x.VendorNos).HasColumnName("vendor_nos");
        builder.Property(x => x.QuoteNos).HasColumnName("quote_nos");
        builder.Property(x => x.OrderNos).HasColumnName("order_nos");
        builder.Property(x => x.InvoiceNos).HasColumnName("invoice_nos");
        builder.Property(x => x.PostedInvoiceNos).HasColumnName("posted_invoice_nos");
        builder.Property(x => x.CreditMemoNos).HasColumnName("credit_memo_nos");
        builder.Property(x => x.PostedCreditMemoNos).HasColumnName("posted_credit_memo_nos");
        builder.Property(x => x.PostedReceiptNos).HasColumnName("posted_receipt_nos");
        builder.Property(x => x.BlanketOrderNos).HasColumnName("blanket_order_nos");
        builder.Property(x => x.CalcInvDiscount).HasColumnName("calc_inv_discount");
        builder.Property(x => x.ApplnBetweenCurrencies).HasColumnName("appln_between_currencies");
        builder.Property(x => x.CopyCommentsBlanketToOrder).HasColumnName("copy_comments_blanket_to_order");
        builder.Property(x => x.CopyCommentsOrderToInvoice).HasColumnName("copy_comments_order_to_invoice");
        builder.Property(x => x.CopyCommentsOrderToReceipt).HasColumnName("copy_comments_order_to_receipt");
        builder.Property(x => x.AllowVatDifference).HasColumnName("allow_vat_difference");
        builder.Property(x => x.CalcInvDiscPerVatId).HasColumnName("calc_inv_disc_per_vat_id");
        builder.Property(x => x.PostedPrepmtInvNos).HasColumnName("posted_prepmt_inv_nos");
        builder.Property(x => x.PostedPrepmtCrMemoNos).HasColumnName("posted_prepmt_cr_memo_nos");
        builder.Property(x => x.CheckPrepmtWhenPosting).HasColumnName("check_prepmt_when_posting");
        builder.Property(x => x.DefaultPostingDate).HasColumnName("default_posting_date");
        builder.Property(x => x.DefaultQtyToReceive).HasColumnName("default_qty_to_receive");
        builder.Property(x => x.ArchiveQuotesAndOrders).HasColumnName("archive_quotes_and_orders");
        builder.Property(x => x.PostWithJobQueue).HasColumnName("post_with_job_queue");
        builder.Property(x => x.JobQueueCategoryCode).HasColumnName("job_queue_category_code");
        builder.Property(x => x.JobQueuePriorityForPost).HasColumnName("job_queue_priority_for_post");
        builder.Property(x => x.PostPrintWithJobQueue).HasColumnName("post_print_with_job_queue");
        builder.Property(x => x.JobQPrioForPostPrint).HasColumnName("job_q_prio_for_post_print");
        builder.Property(x => x.NotifyOnSuccess).HasColumnName("notify_on_success");
        builder.Property(x => x.AllowDocumentDeletionBefore).HasColumnName("allow_document_deletion_before");
        builder.Property(x => x.DebitAccForNonItemLines).HasColumnName("debit_acc_for_non_item_lines");
        builder.Property(x => x.CreditAccForNonItemLines).HasColumnName("credit_acc_for_non_item_lines");
        builder.Property(x => x.PostedReturnShptNos).HasColumnName("posted_return_shpt_nos");
        builder.Property(x => x.CopyCmtsRetOrdToRetShpt).HasColumnName("copy_cmts_ret_ord_to_ret_shpt");
        builder.Property(x => x.CopyCmtsRetOrdToCrMemo).HasColumnName("copy_cmts_ret_ord_to_cr_memo");
        builder.Property(x => x.ReturnOrderNos).HasColumnName("return_order_nos");
        builder.Property(x => x.ReturnShipmentOnCreditMemo).HasColumnName("return_shipment_on_credit_memo");
        builder.Property(x => x.ExactCostReversingMandatory).HasColumnName("exact_cost_reversing_mandatory");
    }
}
