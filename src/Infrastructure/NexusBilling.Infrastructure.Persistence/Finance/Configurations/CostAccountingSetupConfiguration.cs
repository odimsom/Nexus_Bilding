using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class CostAccountingSetupConfiguration : IEntityTypeConfiguration<CostAccountingSetup>
{
    public void Configure(EntityTypeBuilder<CostAccountingSetup> builder)
    {
        builder.ToTable("cost_accounting_setup", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.StartingDateForGLTransfer).HasColumnName("starting_date_for_g_l_transfer");
        builder.Property(x => x.AlignGLAccount).HasColumnName("align_g_l_account");
        builder.Property(x => x.AlignCostCenterDimension).HasColumnName("align_cost_center_dimension");
        builder.Property(x => x.AlignCostObjectDimension).HasColumnName("align_cost_object_dimension");
        builder.Property(x => x.LastAllocationId).HasColumnName("last_allocation_id");
        builder.Property(x => x.LastAllocationDocNo).HasColumnName("last_allocation_doc_no");
        builder.Property(x => x.AutoTransferFromGL).HasColumnName("auto_transfer_from_g_l");
        builder.Property(x => x.CheckGLPostings).HasColumnName("check_g_l_postings");
        builder.Property(x => x.CostCenterDimension).HasColumnName("cost_center_dimension");
        builder.Property(x => x.CostObjectDimension).HasColumnName("cost_object_dimension");
    }
}
