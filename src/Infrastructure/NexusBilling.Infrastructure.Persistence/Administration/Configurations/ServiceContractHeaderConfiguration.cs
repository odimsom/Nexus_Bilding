using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ServiceContractHeaderConfiguration : IEntityTypeConfiguration<ServiceContractHeader>
{
    public void Configure(EntityTypeBuilder<ServiceContractHeader> builder)
    {
        builder.ToTable("service_contract_header", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ContractNo).HasColumnName("contract_no");
        builder.Property(x => x.ContractType).HasColumnName("contract_type");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Description2).HasColumnName("description_2");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.ChangeStatus).HasColumnName("change_status");
        builder.Property(x => x.CustomerNo).HasColumnName("customer_no");
        builder.Property(x => x.ContactName).HasColumnName("contact_name");
        builder.Property(x => x.YourReference).HasColumnName("your_reference");
        builder.Property(x => x.SalespersonCode).HasColumnName("salesperson_code");
        builder.Property(x => x.BillToCustomerNo).HasColumnName("bill_to_customer_no");
        builder.Property(x => x.ShipToCode).HasColumnName("ship_to_code");
        builder.Property(x => x.ServContractAccGrCode).HasColumnName("serv_contract_acc_gr_code");
        builder.Property(x => x.InvoicePeriod).HasColumnName("invoice_period");
        builder.Property(x => x.LastInvoiceDate).HasColumnName("last_invoice_date");
        builder.Property(x => x.NextInvoiceDate).HasColumnName("next_invoice_date");
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
        builder.Property(x => x.ExpirationDate).HasColumnName("expiration_date");
        builder.Property(x => x.FirstServiceDate).HasColumnName("first_service_date");
        builder.Property(x => x.MaxLaborUnitPrice).HasColumnName("max_labor_unit_price").HasPrecision(18, 5);
        builder.Property(x => x.AnnualAmount).HasColumnName("annual_amount").HasPrecision(18, 5);
        builder.Property(x => x.AmountPerPeriod).HasColumnName("amount_per_period").HasPrecision(18, 5);
        builder.Property(x => x.CombineInvoices).HasColumnName("combine_invoices");
        builder.Property(x => x.Prepaid).HasColumnName("prepaid");
        builder.Property(x => x.NextInvoicePeriod).HasColumnName("next_invoice_period");
        builder.Property(x => x.ServiceZoneCode).HasColumnName("service_zone_code");
        builder.Property(x => x.LanguageCode).HasColumnName("language_code");
        builder.Property(x => x.CancelReasonCode).HasColumnName("cancel_reason_code");
        builder.Property(x => x.LastPriceUpdateDate).HasColumnName("last_price_update_date");
        builder.Property(x => x.NextPriceUpdateDate).HasColumnName("next_price_update_date");
        builder.Property(x => x.LastPriceUpdate).HasColumnName("last_price_update").HasPrecision(18, 5);
        builder.Property(x => x.ResponseTimeHours).HasColumnName("response_time_hours").HasPrecision(18, 5);
        builder.Property(x => x.ContractLinesOnInvoice).HasColumnName("contract_lines_on_invoice");
        builder.Property(x => x.ServicePeriod).HasColumnName("service_period");
        builder.Property(x => x.PaymentTermsCode).HasColumnName("payment_terms_code");
        builder.Property(x => x.InvoiceAfterService).HasColumnName("invoice_after_service");
        builder.Property(x => x.QuoteType).HasColumnName("quote_type");
        builder.Property(x => x.AllowUnbalancedAmounts).HasColumnName("allow_unbalanced_amounts");
        builder.Property(x => x.ContractGroupCode).HasColumnName("contract_group_code");
        builder.Property(x => x.ServiceOrderType).HasColumnName("service_order_type");
        builder.Property(x => x.ShortcutDimension1Code).HasColumnName("shortcut_dimension_1_code");
        builder.Property(x => x.ShortcutDimension2Code).HasColumnName("shortcut_dimension_2_code");
        builder.Property(x => x.AcceptBefore).HasColumnName("accept_before");
        builder.Property(x => x.AutomaticCreditMemos).HasColumnName("automatic_credit_memos");
        builder.Property(x => x.TemplateNo).HasColumnName("template_no");
        builder.Property(x => x.PriceUpdatePeriod).HasColumnName("price_update_period");
        builder.Property(x => x.PriceInvIncreaseCode).HasColumnName("price_inv_increase_code");
        builder.Property(x => x.PrintIncreaseText).HasColumnName("print_increase_text");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.Probability).HasColumnName("probability").HasPrecision(18, 5);
        builder.Property(x => x.ResponsibilityCenter).HasColumnName("responsibility_center");
        builder.Property(x => x.PhoneNo).HasColumnName("phone_no");
        builder.Property(x => x.FaxNo).HasColumnName("fax_no");
        builder.Property(x => x.EMail).HasColumnName("e_mail");
        builder.Property(x => x.NextInvoicePeriodStart).HasColumnName("next_invoice_period_start");
        builder.Property(x => x.NextInvoicePeriodEnd).HasColumnName("next_invoice_period_end");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
        builder.Property(x => x.ContactNo).HasColumnName("contact_no");
        builder.Property(x => x.BillToContactNo).HasColumnName("bill_to_contact_no");
        builder.Property(x => x.BillToContact).HasColumnName("bill_to_contact");
        builder.Property(x => x.LastInvoicePeriodEnd).HasColumnName("last_invoice_period_end");
    }
}
