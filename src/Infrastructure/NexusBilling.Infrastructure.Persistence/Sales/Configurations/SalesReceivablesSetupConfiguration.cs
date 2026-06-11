using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class SalesReceivablesSetupConfiguration : IEntityTypeConfiguration<SalesReceivablesSetup>
{
    public void Configure(EntityTypeBuilder<SalesReceivablesSetup> builder)
    {
        builder.ToTable("sales_receivables_setup", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.DiscountPosting).HasColumnName("discount_posting");
        builder.Property(x => x.CreditWarnings).HasColumnName("credit_warnings");
        builder.Property(x => x.StockoutWarning).HasColumnName("stockout_warning");
        builder.Property(x => x.ShipmentOnInvoice).HasColumnName("shipment_on_invoice");
        builder.Property(x => x.InvoiceRounding).HasColumnName("invoice_rounding");
        builder.Property(x => x.ExtDocNoMandatory).HasColumnName("ext_doc_no_mandatory");
        builder.Property(x => x.CustomerNos).HasColumnName("customer_nos");
        builder.Property(x => x.QuoteNos).HasColumnName("quote_nos");
        builder.Property(x => x.OrderNos).HasColumnName("order_nos");
        builder.Property(x => x.InvoiceNos).HasColumnName("invoice_nos");
        builder.Property(x => x.PostedInvoiceNos).HasColumnName("posted_invoice_nos");
        builder.Property(x => x.CreditMemoNos).HasColumnName("credit_memo_nos");
        builder.Property(x => x.PostedCreditMemoNos).HasColumnName("posted_credit_memo_nos");
        builder.Property(x => x.PostedShipmentNos).HasColumnName("posted_shipment_nos");
        builder.Property(x => x.ReminderNos).HasColumnName("reminder_nos");
        builder.Property(x => x.IssuedReminderNos).HasColumnName("issued_reminder_nos");
        builder.Property(x => x.FinChrgMemoNos).HasColumnName("fin_chrg_memo_nos");
        builder.Property(x => x.IssuedFinChrgMNos).HasColumnName("issued_fin_chrg_m_nos");
        builder.Property(x => x.PostedPrepmtInvNos).HasColumnName("posted_prepmt_inv_nos");
        builder.Property(x => x.PostedPrepmtCrMemoNos).HasColumnName("posted_prepmt_cr_memo_nos");
        builder.Property(x => x.BlanketOrderNos).HasColumnName("blanket_order_nos");
        builder.Property(x => x.CalcInvDiscount).HasColumnName("calc_inv_discount");
        builder.Property(x => x.ApplnBetweenCurrencies).HasColumnName("appln_between_currencies");
        builder.Property(x => x.CopyCommentsBlanketToOrder).HasColumnName("copy_comments_blanket_to_order");
        builder.Property(x => x.CopyCommentsOrderToInvoice).HasColumnName("copy_comments_order_to_invoice");
        builder.Property(x => x.CopyCommentsOrderToShpt).HasColumnName("copy_comments_order_to_shpt");
        builder.Property(x => x.AllowVatDifference).HasColumnName("allow_vat_difference");
        builder.Property(x => x.CalcInvDiscPerVatId).HasColumnName("calc_inv_disc_per_vat_id");
        builder.Property(x => x.LogoPositionOnDocuments).HasColumnName("logo_position_on_documents");
        builder.Property(x => x.CheckPrepmtWhenPosting).HasColumnName("check_prepmt_when_posting");
        builder.Property(x => x.DefaultPostingDate).HasColumnName("default_posting_date");
        builder.Property(x => x.DefaultQuantityToShip).HasColumnName("default_quantity_to_ship");
        builder.Property(x => x.ArchiveQuotesAndOrders).HasColumnName("archive_quotes_and_orders");
        builder.Property(x => x.PostWithJobQueue).HasColumnName("post_with_job_queue");
        builder.Property(x => x.JobQueueCategoryCode).HasColumnName("job_queue_category_code");
        builder.Property(x => x.JobQueuePriorityForPost).HasColumnName("job_queue_priority_for_post");
        builder.Property(x => x.PostPrintWithJobQueue).HasColumnName("post_print_with_job_queue");
        builder.Property(x => x.JobQPrioForPostPrint).HasColumnName("job_q_prio_for_post_print");
        builder.Property(x => x.NotifyOnSuccess).HasColumnName("notify_on_success");
        builder.Property(x => x.VatBusPostingGrPrice).HasColumnName("vat_bus_posting_gr_price");
        builder.Property(x => x.DirectDebitMandateNos).HasColumnName("direct_debit_mandate_nos");
        builder.Property(x => x.AllowDocumentDeletionBefore).HasColumnName("allow_document_deletion_before");
        builder.Property(x => x.DefaultItemQuantity).HasColumnName("default_item_quantity");
        builder.Property(x => x.CreateItemFromDescription).HasColumnName("create_item_from_description");
        builder.Property(x => x.PostedReturnReceiptNos).HasColumnName("posted_return_receipt_nos");
        builder.Property(x => x.CopyCmtsRetOrdToRetRcpt).HasColumnName("copy_cmts_ret_ord_to_ret_rcpt");
        builder.Property(x => x.CopyCmtsRetOrdToCrMemo).HasColumnName("copy_cmts_ret_ord_to_cr_memo");
        builder.Property(x => x.ReturnOrderNos).HasColumnName("return_order_nos");
        builder.Property(x => x.ReturnReceiptOnCreditMemo).HasColumnName("return_receipt_on_credit_memo");
        builder.Property(x => x.ExactCostReversingMandatory).HasColumnName("exact_cost_reversing_mandatory");
        builder.Property(x => x.CustomerGroupDimensionCode).HasColumnName("customer_group_dimension_code");
        builder.Property(x => x.SalespersonDimensionCode).HasColumnName("salesperson_dimension_code");
        builder.Property(x => x.FreightGLAccNo).HasColumnName("freight_g_l_acc_no");
    }
}
