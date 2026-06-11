using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class GLBudgetNameConfiguration : IEntityTypeConfiguration<GLBudgetName>
{
    public void Configure(EntityTypeBuilder<GLBudgetName> builder)
    {
        builder.ToTable("gl_budget_name", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Blocked).HasColumnName("blocked");
        builder.Property(x => x.BudgetDimension1Code).HasColumnName("budget_dimension1_code");
        builder.Property(x => x.BudgetDimension2Code).HasColumnName("budget_dimension2_code");
        builder.Property(x => x.BudgetDimension3Code).HasColumnName("budget_dimension3_code");
        builder.Property(x => x.BudgetDimension4Code).HasColumnName("budget_dimension4_code");
    }
}
