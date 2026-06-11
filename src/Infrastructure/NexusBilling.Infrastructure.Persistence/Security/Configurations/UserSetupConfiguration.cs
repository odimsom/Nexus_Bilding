using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class UserSetupConfiguration : IEntityTypeConfiguration<UserSetup>
{
    public void Configure(EntityTypeBuilder<UserSetup> builder)
    {
        builder.ToTable("user_setup", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.AllowPostingFrom).HasColumnName("allow_posting_from");
        builder.Property(x => x.AllowPostingTo).HasColumnName("allow_posting_to");
        builder.Property(x => x.RegisterTime).HasColumnName("register_time");
        builder.Property(x => x.SalespersPurchCode).HasColumnName("salespers_purch_code");
        builder.Property(x => x.ApproverId).HasColumnName("approver_id");
        builder.Property(x => x.SalesAmountApprovalLimit).HasColumnName("sales_amount_approval_limit");
        builder.Property(x => x.PurchaseAmountApprovalLimit).HasColumnName("purchase_amount_approval_limit");
        builder.Property(x => x.UnlimitedSalesApproval).HasColumnName("unlimited_sales_approval");
        builder.Property(x => x.UnlimitedPurchaseApproval).HasColumnName("unlimited_purchase_approval");
        builder.Property(x => x.Substitute).HasColumnName("substitute");
        builder.Property(x => x.EMail).HasColumnName("e_mail");
        builder.Property(x => x.RequestAmountApprovalLimit).HasColumnName("request_amount_approval_limit");
        builder.Property(x => x.UnlimitedRequestApproval).HasColumnName("unlimited_request_approval");
        builder.Property(x => x.ApprovalAdministrator).HasColumnName("approval_administrator");
        builder.Property(x => x.TimeSheetAdmin).HasColumnName("time_sheet_admin");
        builder.Property(x => x.AllowFaPostingFrom).HasColumnName("allow_fa_posting_from");
        builder.Property(x => x.AllowFaPostingTo).HasColumnName("allow_fa_posting_to");
        builder.Property(x => x.SalesRespCtrFilter).HasColumnName("sales_resp_ctr_filter");
        builder.Property(x => x.PurchaseRespCtrFilter).HasColumnName("purchase_resp_ctr_filter");
        builder.Property(x => x.ServiceRespCtrFilter).HasColumnName("service_resp_ctr_filter");
    }
}
