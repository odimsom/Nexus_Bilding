using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class IssuedFinChargeMemoHeaderConfiguration : IEntityTypeConfiguration<IssuedFinChargeMemoHeader>
{
    public void Configure(EntityTypeBuilder<IssuedFinChargeMemoHeader> builder)
    {
        builder.ToTable("issued_fin_charge_memo_header", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.CustomerNo).HasColumnName("customer_no");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Name2).HasColumnName("name_2");
        builder.Property(x => x.Address).HasColumnName("address");
        builder.Property(x => x.Address2).HasColumnName("address_2");
        builder.Property(x => x.PostCode).HasColumnName("post_code");
        builder.Property(x => x.City).HasColumnName("city");
        builder.Property(x => x.County).HasColumnName("county");
        builder.Property(x => x.CountryRegionCode).HasColumnName("country_region_code");
        builder.Property(x => x.LanguageCode).HasColumnName("language_code");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.Contact).HasColumnName("contact");
        builder.Property(x => x.YourReference).HasColumnName("your_reference");
        builder.Property(x => x.ShortcutDimension1Code).HasColumnName("shortcut_dimension_1_code");
        builder.Property(x => x.ShortcutDimension2Code).HasColumnName("shortcut_dimension_2_code");
        builder.Property(x => x.CustomerPostingGroup).HasColumnName("customer_posting_group");
        builder.Property(x => x.GenBusPostingGroup).HasColumnName("gen_bus_posting_group");
        builder.Property(x => x.VatRegistrationNo).HasColumnName("vat_registration_no");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.DocumentDate).HasColumnName("document_date");
        builder.Property(x => x.DueDate).HasColumnName("due_date");
        builder.Property(x => x.FinChargeTermsCode).HasColumnName("fin_charge_terms_code");
        builder.Property(x => x.InterestPosted).HasColumnName("interest_posted");
        builder.Property(x => x.AdditionalFeePosted).HasColumnName("additional_fee_posted");
        builder.Property(x => x.PostingDescription).HasColumnName("posting_description");
        builder.Property(x => x.NoPrinted).HasColumnName("no_printed");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.PreAssignedNoSeries).HasColumnName("pre_assigned_no_series");
        builder.Property(x => x.PreAssignedNo).HasColumnName("pre_assigned_no");
        builder.Property(x => x.SourceCode).HasColumnName("source_code");
        builder.Property(x => x.TaxAreaCode).HasColumnName("tax_area_code");
        builder.Property(x => x.TaxLiable).HasColumnName("tax_liable");
        builder.Property(x => x.VatBusPostingGroup).HasColumnName("vat_bus_posting_group");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
    }
}
