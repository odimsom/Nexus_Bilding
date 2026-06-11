using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class MarketingSetupConfiguration : IEntityTypeConfiguration<MarketingSetup>
{
    public void Configure(EntityTypeBuilder<MarketingSetup> builder)
    {
        builder.ToTable("marketing_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.ContactNos).HasColumnName("contact_nos");
        builder.Property(x => x.CampaignNos).HasColumnName("campaign_nos");
        builder.Property(x => x.SegmentNos).HasColumnName("segment_nos");
        builder.Property(x => x.ToDoNos).HasColumnName("to_do_nos");
        builder.Property(x => x.OpportunityNos).HasColumnName("opportunity_nos");
        builder.Property(x => x.BusRelCodeForCustomers).HasColumnName("bus_rel_code_for_customers");
        builder.Property(x => x.BusRelCodeForVendors).HasColumnName("bus_rel_code_for_vendors");
        builder.Property(x => x.BusRelCodeForBankAccs).HasColumnName("bus_rel_code_for_bank_accs");
        builder.Property(x => x.InheritSalespersonCode).HasColumnName("inherit_salesperson_code");
        builder.Property(x => x.InheritTerritoryCode).HasColumnName("inherit_territory_code");
        builder.Property(x => x.InheritCountryRegionCode).HasColumnName("inherit_country_region_code");
        builder.Property(x => x.InheritLanguageCode).HasColumnName("inherit_language_code");
        builder.Property(x => x.InheritAddressDetails).HasColumnName("inherit_address_details");
        builder.Property(x => x.InheritCommunicationDetails).HasColumnName("inherit_communication_details");
        builder.Property(x => x.DefaultSalespersonCode).HasColumnName("default_salesperson_code");
        builder.Property(x => x.DefaultTerritoryCode).HasColumnName("default_territory_code");
        builder.Property(x => x.DefaultCountryRegionCode).HasColumnName("default_country_region_code");
        builder.Property(x => x.DefaultLanguageCode).HasColumnName("default_language_code");
        builder.Property(x => x.DefaultSalesCycleCode).HasColumnName("default_sales_cycle_code");
        builder.Property(x => x.AttachmentStorageType).HasColumnName("attachment_storage_type");
        builder.Property(x => x.AttachmentStorageLocation).HasColumnName("attachment_storage_location");
        builder.Property(x => x.AutosearchForDuplicates).HasColumnName("autosearch_for_duplicates");
        builder.Property(x => x.SearchHit).HasColumnName("search_hit");
        builder.Property(x => x.MaintainDuplSearchStrings).HasColumnName("maintain_dupl_search_strings");
        builder.Property(x => x.MergefieldLanguageId).HasColumnName("mergefield_language_id");
        builder.Property(x => x.DefCompanySalutationCode).HasColumnName("def_company_salutation_code");
        builder.Property(x => x.DefaultPersonSalutationCode).HasColumnName("default_person_salutation_code");
        builder.Property(x => x.DefaultCorrespondenceType).HasColumnName("default_correspondence_type");
        builder.Property(x => x.QueueFolderPath).HasColumnName("queue_folder_path");
        builder.Property(x => x.QueueFolderUid).HasColumnName("queue_folder_uid");
        builder.Property(x => x.StorageFolderPath).HasColumnName("storage_folder_path");
        builder.Property(x => x.StorageFolderUid).HasColumnName("storage_folder_uid");
        builder.Property(x => x.DefaultToDoDateCalculation).HasColumnName("default_to_do_date_calculation");
        builder.Property(x => x.AutodiscoveryEMailAddress).HasColumnName("autodiscovery_e_mail_address");
        builder.Property(x => x.EmailBatchSize).HasColumnName("email_batch_size");
        builder.Property(x => x.ExchangeServiceUrl).HasColumnName("exchange_service_url");
        builder.Property(x => x.ExchangeAccountUserName).HasColumnName("exchange_account_user_name");
        builder.Property(x => x.ExchangeAccountPasswordKey).HasColumnName("exchange_account_password_key");
    }
}
