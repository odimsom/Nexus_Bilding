using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("job", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.SearchDescription).HasColumnName("search_description");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Description2).HasColumnName("description_2");
        builder.Property(x => x.BillToCustomerNo).HasColumnName("bill_to_customer_no");
        builder.Property(x => x.CreationDate).HasColumnName("creation_date");
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
        builder.Property(x => x.EndingDate).HasColumnName("ending_date");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.PersonResponsible).HasColumnName("person_responsible");
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension_1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension_2_code");
        builder.Property(x => x.JobPostingGroup).HasColumnName("job_posting_group");
        builder.Property(x => x.Blocked).HasColumnName("blocked");
        builder.Property(x => x.LastDateModified).HasColumnName("last_date_modified");
        builder.Property(x => x.CustomerDiscGroup).HasColumnName("customer_disc_group");
        builder.Property(x => x.CustomerPriceGroup).HasColumnName("customer_price_group");
        builder.Property(x => x.LanguageCode).HasColumnName("language_code");
        builder.Property(x => x.Picture).HasColumnName("picture");
        builder.Property(x => x.BillToName).HasColumnName("bill_to_name");
        builder.Property(x => x.BillToAddress).HasColumnName("bill_to_address");
        builder.Property(x => x.BillToAddress2).HasColumnName("bill_to_address_2");
        builder.Property(x => x.BillToCity).HasColumnName("bill_to_city");
        builder.Property(x => x.BillToCounty).HasColumnName("bill_to_county");
        builder.Property(x => x.BillToPostCode).HasColumnName("bill_to_post_code");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.BillToCountryRegionCode).HasColumnName("bill_to_country_region_code");
        builder.Property(x => x.BillToName2).HasColumnName("bill_to_name_2");
        builder.Property(x => x.Reserve).HasColumnName("reserve");
        builder.Property(x => x.Image).HasColumnName("image");
        builder.Property(x => x.WipMethod).HasColumnName("wip_method");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.BillToContactNo).HasColumnName("bill_to_contact_no");
        builder.Property(x => x.BillToContact).HasColumnName("bill_to_contact");
        builder.Property(x => x.WipPostingDate).HasColumnName("wip_posting_date");
        builder.Property(x => x.InvoiceCurrencyCode).HasColumnName("invoice_currency_code");
        builder.Property(x => x.ExchCalculationCost).HasColumnName("exch_calculation_cost");
        builder.Property(x => x.ExchCalculationPrice).HasColumnName("exch_calculation_price");
        builder.Property(x => x.AllowScheduleContractLines).HasColumnName("allow_schedule_contract_lines");
        builder.Property(x => x.Complete).HasColumnName("complete");
        builder.Property(x => x.ApplyUsageLink).HasColumnName("apply_usage_link");
        builder.Property(x => x.WipPostingMethod).HasColumnName("wip_posting_method");
        builder.Property(x => x.OverBudget).HasColumnName("over_budget");
        builder.Property(x => x.ProjectManager).HasColumnName("project_manager");
    }
}
