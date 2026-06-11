using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class VatEntryConfiguration : IEntityTypeConfiguration<VatEntry>
{
    public void Configure(EntityTypeBuilder<VatEntry> builder)
    {
        builder.ToTable("vat_entry", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.GenBusPostingGroup).HasColumnName("gen_bus_posting_group");
        builder.Property(x => x.GenProdPostingGroup).HasColumnName("gen_prod_posting_group");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.Base).HasColumnName("base").HasPrecision(18, 5);
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 5);
        builder.Property(x => x.VatCalculationType).HasColumnName("vat_calculation_type");
        builder.Property(x => x.BillToPayToNo).HasColumnName("bill_to_pay_to_no");
        builder.Property(x => x.Eu3PartyTrade).HasColumnName("eu_3_party_trade");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.SourceCode).HasColumnName("source_code");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.ClosedByEntryNo).HasColumnName("closed_by_entry_no");
        builder.Property(x => x.Closed).HasColumnName("closed");
        builder.Property(x => x.CountryRegionCode).HasColumnName("country_region_code");
        builder.Property(x => x.InternalRefNo).HasColumnName("internal_ref_no");
        builder.Property(x => x.TransactionNo).HasColumnName("transaction_no");
        builder.Property(x => x.UnrealizedAmount).HasColumnName("unrealized_amount").HasPrecision(18, 5);
        builder.Property(x => x.UnrealizedBase).HasColumnName("unrealized_base").HasPrecision(18, 5);
        builder.Property(x => x.RemainingUnrealizedAmount).HasColumnName("remaining_unrealized_amount").HasPrecision(18, 5);
        builder.Property(x => x.RemainingUnrealizedBase).HasColumnName("remaining_unrealized_base").HasPrecision(18, 5);
        builder.Property(x => x.ExternalDocumentNo).HasColumnName("external_document_no");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.TaxAreaCode).HasColumnName("tax_area_code");
        builder.Property(x => x.TaxLiable).HasColumnName("tax_liable");
        builder.Property(x => x.TaxGroupCode).HasColumnName("tax_group_code");
        builder.Property(x => x.UseTax).HasColumnName("use_tax");
        builder.Property(x => x.TaxJurisdictionCode).HasColumnName("tax_jurisdiction_code");
        builder.Property(x => x.TaxGroupUsed).HasColumnName("tax_group_used");
        builder.Property(x => x.TaxType).HasColumnName("tax_type");
        builder.Property(x => x.TaxOnTax).HasColumnName("tax_on_tax");
        builder.Property(x => x.SalesTaxConnectionNo).HasColumnName("sales_tax_connection_no");
        builder.Property(x => x.UnrealizedVatEntryNo).HasColumnName("unrealized_vat_entry_no");
        builder.Property(x => x.VatBusPostingGroup).HasColumnName("vat_bus_posting_group");
        builder.Property(x => x.VatProdPostingGroup).HasColumnName("vat_prod_posting_group");
        builder.Property(x => x.AdditionalCurrencyAmount).HasColumnName("additional_currency_amount").HasPrecision(18, 5);
        builder.Property(x => x.AdditionalCurrencyBase).HasColumnName("additional_currency_base").HasPrecision(18, 5);
        builder.Property(x => x.AddCurrencyUnrealizedAmt).HasColumnName("add_currency_unrealized_amt").HasPrecision(18, 5);
        builder.Property(x => x.AddCurrencyUnrealizedBase).HasColumnName("add_currency_unrealized_base").HasPrecision(18, 5);
        builder.Property(x => x.VatBaseDiscount).HasColumnName("vat_base_discount").HasPrecision(18, 5);
        builder.Property(x => x.AddCurrRemUnrealAmount).HasColumnName("add_curr_rem_unreal_amount").HasPrecision(18, 5);
        builder.Property(x => x.AddCurrRemUnrealBase).HasColumnName("add_curr_rem_unreal_base").HasPrecision(18, 5);
        builder.Property(x => x.VatDifference).HasColumnName("vat_difference").HasPrecision(18, 5);
        builder.Property(x => x.AddCurrVatDifference).HasColumnName("add_curr_vat_difference").HasPrecision(18, 5);
        builder.Property(x => x.ShipToOrderAddressCode).HasColumnName("ship_to_order_address_code");
        builder.Property(x => x.DocumentDate).HasColumnName("document_date");
        builder.Property(x => x.VatRegistrationNo).HasColumnName("vat_registration_no");
        builder.Property(x => x.Reversed).HasColumnName("reversed");
        builder.Property(x => x.ReversedByEntryNo).HasColumnName("reversed_by_entry_no");
        builder.Property(x => x.ReversedEntryNo).HasColumnName("reversed_entry_no");
        builder.Property(x => x.EuService).HasColumnName("eu_service");
    }
}
