using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ExcelBufferConfiguration : IEntityTypeConfiguration<ExcelBuffer>
{
    public void Configure(EntityTypeBuilder<ExcelBuffer> builder)
    {
        builder.ToTable("excel_buffer", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.RowNo).HasColumnName("row_no");
        builder.Property(x => x.Xlrowid).HasColumnName("xlrowid");
        builder.Property(x => x.ColumnNo).HasColumnName("column_no");
        builder.Property(x => x.Xlcolid).HasColumnName("xlcolid");
        builder.Property(x => x.CellValueAsText).HasColumnName("cell_value_as_text");
        builder.Property(x => x.Comment).HasColumnName("comment");
        builder.Property(x => x.Formula).HasColumnName("formula");
        builder.Property(x => x.Bold).HasColumnName("bold");
        builder.Property(x => x.Italic).HasColumnName("italic");
        builder.Property(x => x.Underline).HasColumnName("underline");
        builder.Property(x => x.Numberformat).HasColumnName("numberformat");
        builder.Property(x => x.Formula2).HasColumnName("formula2");
        builder.Property(x => x.Formula3).HasColumnName("formula3");
        builder.Property(x => x.Formula4).HasColumnName("formula4");
        builder.Property(x => x.CellType).HasColumnName("cell_type");
        builder.Property(x => x.DoubleUnderline).HasColumnName("double_underline");
    }
}
