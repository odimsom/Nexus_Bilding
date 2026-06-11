using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class InteractionTemplateSetupConfiguration : IEntityTypeConfiguration<InteractionTemplateSetup>
{
    public void Configure(EntityTypeBuilder<InteractionTemplateSetup> builder)
    {
        builder.ToTable("interaction_template_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.SalesInvoices).HasColumnName("sales_invoices");
        builder.Property(x => x.SalesCrMemo).HasColumnName("sales_cr_memo");
        builder.Property(x => x.SalesOrdCnfrmn).HasColumnName("sales_ord_cnfrmn");
        builder.Property(x => x.SalesQuotes).HasColumnName("sales_quotes");
        builder.Property(x => x.PurchInvoices).HasColumnName("purch_invoices");
        builder.Property(x => x.PurchCrMemos).HasColumnName("purch_cr_memos");
        builder.Property(x => x.PurchOrders).HasColumnName("purch_orders");
        builder.Property(x => x.PurchQuotes).HasColumnName("purch_quotes");
        builder.Property(x => x.EMails).HasColumnName("e_mails");
        builder.Property(x => x.CoverSheets).HasColumnName("cover_sheets");
        builder.Property(x => x.OutgCalls).HasColumnName("outg_calls");
        builder.Property(x => x.SalesBlnktOrd).HasColumnName("sales_blnkt_ord");
        builder.Property(x => x.ServOrdPost).HasColumnName("serv_ord_post");
        builder.Property(x => x.SalesShptNote).HasColumnName("sales_shpt_note");
        builder.Property(x => x.SalesStatement).HasColumnName("sales_statement");
        builder.Property(x => x.SalesRmdr).HasColumnName("sales_rmdr");
        builder.Property(x => x.ServOrdCreate).HasColumnName("serv_ord_create");
        builder.Property(x => x.PurchBlnktOrd).HasColumnName("purch_blnkt_ord");
        builder.Property(x => x.PurchRcpt).HasColumnName("purch_rcpt");
        builder.Property(x => x.SalesReturnOrder).HasColumnName("sales_return_order");
        builder.Property(x => x.SalesReturnReceipt).HasColumnName("sales_return_receipt");
        builder.Property(x => x.SalesFinanceChargeMemo).HasColumnName("sales_finance_charge_memo");
        builder.Property(x => x.PurchReturnShipment).HasColumnName("purch_return_shipment");
        builder.Property(x => x.PurchReturnOrdCnfrmn).HasColumnName("purch_return_ord_cnfrmn");
        builder.Property(x => x.ServiceContract).HasColumnName("service_contract");
        builder.Property(x => x.ServiceContractQuote).HasColumnName("service_contract_quote");
        builder.Property(x => x.ServiceQuote).HasColumnName("service_quote");
        builder.Property(x => x.MeetingInvitation).HasColumnName("meeting_invitation");
    }
}
