using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ServiceItemConfiguration : IEntityTypeConfiguration<ServiceItem>
{
    public void Configure(EntityTypeBuilder<ServiceItem> builder)
    {
        builder.ToTable("service_item", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.SerialNo).HasColumnName("serial_no");
        builder.Property(x => x.ServiceItemGroupCode).HasColumnName("service_item_group_code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Description2).HasColumnName("description_2");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.Priority).HasColumnName("priority");
        builder.Property(x => x.CustomerNo).HasColumnName("customer_no");
        builder.Property(x => x.ShipToCode).HasColumnName("ship_to_code");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.LocationOfServiceItem).HasColumnName("location_of_service_item");
        builder.Property(x => x.SalesUnitPrice).HasColumnName("sales_unit_price").HasPrecision(18, 5);
        builder.Property(x => x.SalesUnitCost).HasColumnName("sales_unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.WarrantyStartingDateLabor).HasColumnName("warranty_starting_date_labor");
        builder.Property(x => x.WarrantyEndingDateLabor).HasColumnName("warranty_ending_date_labor");
        builder.Property(x => x.WarrantyStartingDateParts).HasColumnName("warranty_starting_date_parts");
        builder.Property(x => x.WarrantyEndingDateParts).HasColumnName("warranty_ending_date_parts");
        builder.Property(x => x.WarrantyParts).HasColumnName("warranty_parts").HasPrecision(18, 5);
        builder.Property(x => x.WarrantyLabor).HasColumnName("warranty_labor").HasPrecision(18, 5);
        builder.Property(x => x.ResponseTimeHours).HasColumnName("response_time_hours").HasPrecision(18, 5);
        builder.Property(x => x.InstallationDate).HasColumnName("installation_date");
        builder.Property(x => x.SalesDate).HasColumnName("sales_date");
        builder.Property(x => x.LastServiceDate).HasColumnName("last_service_date");
        builder.Property(x => x.DefaultContractValue).HasColumnName("default_contract_value").HasPrecision(18, 5);
        builder.Property(x => x.DefaultContractDiscount).HasColumnName("default_contract_discount").HasPrecision(18, 5);
        builder.Property(x => x.VendorNo).HasColumnName("vendor_no");
        builder.Property(x => x.VendorItemNo).HasColumnName("vendor_item_no");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.VendorItemName).HasColumnName("vendor_item_name");
        builder.Property(x => x.PreferredResource).HasColumnName("preferred_resource");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.ServicePriceGroupCode).HasColumnName("service_price_group_code");
        builder.Property(x => x.DefaultContractCost).HasColumnName("default_contract_cost").HasPrecision(18, 5);
        builder.Property(x => x.SearchDescription).HasColumnName("search_description");
        builder.Property(x => x.SalesServShptDocumentNo).HasColumnName("sales_serv_shpt_document_no");
        builder.Property(x => x.SalesServShptLineNo).HasColumnName("sales_serv_shpt_line_no");
        builder.Property(x => x.ShipmentType).HasColumnName("shipment_type");
    }
}
