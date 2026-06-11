using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ManufacturingUserTemplateConfiguration : IEntityTypeConfiguration<ManufacturingUserTemplate>
{
    public void Configure(EntityTypeBuilder<ManufacturingUserTemplate> builder)
    {
        builder.ToTable("manufacturing_user_template", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.CreatePurchaseOrder).HasColumnName("create_purchase_order");
        builder.Property(x => x.CreateProductionOrder).HasColumnName("create_production_order");
        builder.Property(x => x.CreateTransferOrder).HasColumnName("create_transfer_order");
        builder.Property(x => x.CreateAssemblyOrder).HasColumnName("create_assembly_order");
        builder.Property(x => x.PurchaseReqWkshTemplate).HasColumnName("purchase_req_wksh_template");
        builder.Property(x => x.PurchaseWkshName).HasColumnName("purchase_wksh_name");
        builder.Property(x => x.ProdReqWkshTemplate).HasColumnName("prod_req_wksh_template");
        builder.Property(x => x.ProdWkshName).HasColumnName("prod_wksh_name");
        builder.Property(x => x.TransferReqWkshTemplate).HasColumnName("transfer_req_wksh_template");
        builder.Property(x => x.TransferWkshName).HasColumnName("transfer_wksh_name");
        builder.Property(x => x.MakeOrders).HasColumnName("make_orders");
    }
}
