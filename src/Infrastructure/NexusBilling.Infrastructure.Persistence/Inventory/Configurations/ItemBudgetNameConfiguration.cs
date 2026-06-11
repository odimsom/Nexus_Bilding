using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ItemBudgetNameConfiguration : IEntityTypeConfiguration<ItemBudgetName>
{
    public void Configure(EntityTypeBuilder<ItemBudgetName> builder)
    {
        builder.ToTable("item_budget_name", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.AnalysisArea).HasColumnName("analysis_area");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Blocked).HasColumnName("blocked");
        builder.Property(x => x.BudgetDimension1Code).HasColumnName("budget_dimension_1_code");
        builder.Property(x => x.BudgetDimension2Code).HasColumnName("budget_dimension_2_code");
        builder.Property(x => x.BudgetDimension3Code).HasColumnName("budget_dimension_3_code");
    }
}
