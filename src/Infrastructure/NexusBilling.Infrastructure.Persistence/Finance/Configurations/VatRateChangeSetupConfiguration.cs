using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class VatRateChangeSetupConfiguration : IEntityTypeConfiguration<VatRateChangeSetup>
{
    public void Configure(EntityTypeBuilder<VatRateChangeSetup> builder)
    {
        builder.ToTable("vat_rate_change_setup", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.UpdateGenProdPostGroups).HasColumnName("update_gen_prod_post_groups");
        builder.Property(x => x.UpdateGLAccounts).HasColumnName("update_g_l_accounts");
        builder.Property(x => x.UpdateItems).HasColumnName("update_items");
        builder.Property(x => x.UpdateItemTemplates).HasColumnName("update_item_templates");
        builder.Property(x => x.UpdateItemCharges).HasColumnName("update_item_charges");
        builder.Property(x => x.UpdateResources).HasColumnName("update_resources");
        builder.Property(x => x.UpdateGenJournalLines).HasColumnName("update_gen_journal_lines");
        builder.Property(x => x.UpdateGenJournalAllocation).HasColumnName("update_gen_journal_allocation");
        builder.Property(x => x.UpdateStdGenJnlLines).HasColumnName("update_std_gen_jnl_lines");
        builder.Property(x => x.UpdateResJournalLines).HasColumnName("update_res_journal_lines");
        builder.Property(x => x.UpdateJobJournalLines).HasColumnName("update_job_journal_lines");
        builder.Property(x => x.UpdateRequisitionLines).HasColumnName("update_requisition_lines");
        builder.Property(x => x.UpdateStdItemJnlLines).HasColumnName("update_std_item_jnl_lines");
        builder.Property(x => x.UpdateServiceDocs).HasColumnName("update_service_docs");
        builder.Property(x => x.UpdateServPriceAdjDetail).HasColumnName("update_serv_price_adj_detail");
        builder.Property(x => x.UpdateSalesDocuments).HasColumnName("update_sales_documents");
        builder.Property(x => x.UpdatePurchaseDocuments).HasColumnName("update_purchase_documents");
        builder.Property(x => x.UpdateProductionOrders).HasColumnName("update_production_orders");
        builder.Property(x => x.UpdateWorkCenters).HasColumnName("update_work_centers");
        builder.Property(x => x.UpdateMachineCenters).HasColumnName("update_machine_centers");
        builder.Property(x => x.UpdateReminders).HasColumnName("update_reminders");
        builder.Property(x => x.UpdateFinanceChargeMemos).HasColumnName("update_finance_charge_memos");
        builder.Property(x => x.VatRateChangeToolCompleted).HasColumnName("vat_rate_change_tool_completed");
        builder.Property(x => x.IgnoreStatusOnSalesDocs).HasColumnName("ignore_status_on_sales_docs");
        builder.Property(x => x.IgnoreStatusOnPurchDocs).HasColumnName("ignore_status_on_purch_docs");
        builder.Property(x => x.PerformConversion).HasColumnName("perform_conversion");
        builder.Property(x => x.ItemFilter).HasColumnName("item_filter");
        builder.Property(x => x.AccountFilter).HasColumnName("account_filter");
        builder.Property(x => x.ResourceFilter).HasColumnName("resource_filter");
    }
}
