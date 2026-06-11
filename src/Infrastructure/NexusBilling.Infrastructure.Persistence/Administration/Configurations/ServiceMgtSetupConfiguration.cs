using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ServiceMgtSetupConfiguration : IEntityTypeConfiguration<ServiceMgtSetup>
{
    public void Configure(EntityTypeBuilder<ServiceMgtSetup> builder)
    {
        builder.ToTable("service_mgt_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.FaultReportingLevel).HasColumnName("fault_reporting_level");
        builder.Property(x => x.LinkServiceToServiceItem).HasColumnName("link_service_to_service_item");
        builder.Property(x => x.SalespersonMandatory).HasColumnName("salesperson_mandatory");
        builder.Property(x => x.WarrantyDiscParts).HasColumnName("warranty_disc_parts").HasPrecision(18, 5);
        builder.Property(x => x.WarrantyDiscLabor).HasColumnName("warranty_disc_labor").HasPrecision(18, 5);
        builder.Property(x => x.ContractRspTimeMandatory).HasColumnName("contract_rsp_time_mandatory");
        builder.Property(x => x.ServiceOrderStartingFee).HasColumnName("service_order_starting_fee");
        builder.Property(x => x.RegisterContractChanges).HasColumnName("register_contract_changes");
        builder.Property(x => x.ContractInvLineTextCode).HasColumnName("contract_inv_line_text_code");
        builder.Property(x => x.ContractLineInvTextCode).HasColumnName("contract_line_inv_text_code");
        builder.Property(x => x.ContractInvPeriodTextCode).HasColumnName("contract_inv_period_text_code");
        builder.Property(x => x.ContractCreditLineTextCode).HasColumnName("contract_credit_line_text_code");
        builder.Property(x => x.SendFirstWarningTo).HasColumnName("send_first_warning_to");
        builder.Property(x => x.SendSecondWarningTo).HasColumnName("send_second_warning_to");
        builder.Property(x => x.SendThirdWarningTo).HasColumnName("send_third_warning_to");
        builder.Property(x => x.FirstWarningWithinHours).HasColumnName("first_warning_within_hours").HasPrecision(18, 5);
        builder.Property(x => x.SecondWarningWithinHours).HasColumnName("second_warning_within_hours").HasPrecision(18, 5);
        builder.Property(x => x.ThirdWarningWithinHours).HasColumnName("third_warning_within_hours").HasPrecision(18, 5);
        builder.Property(x => x.NextServiceCalcMethod).HasColumnName("next_service_calc_method");
        builder.Property(x => x.ServiceOrderTypeMandatory).HasColumnName("service_order_type_mandatory");
        builder.Property(x => x.ServiceZonesOption).HasColumnName("service_zones_option");
        builder.Property(x => x.ServiceOrderStartMandatory).HasColumnName("service_order_start_mandatory");
        builder.Property(x => x.ServiceOrderFinishMandatory).HasColumnName("service_order_finish_mandatory");
        builder.Property(x => x.ResourceSkillsOption).HasColumnName("resource_skills_option");
        builder.Property(x => x.OneServiceItemLineOrder).HasColumnName("one_service_item_line_order");
        builder.Property(x => x.UnitOfMeasureMandatory).HasColumnName("unit_of_measure_mandatory");
        builder.Property(x => x.FaultReasonCodeMandatory).HasColumnName("fault_reason_code_mandatory");
        builder.Property(x => x.ContractServOrdMaxDays).HasColumnName("contract_serv_ord_max_days");
        builder.Property(x => x.LastContractServiceDate).HasColumnName("last_contract_service_date");
        builder.Property(x => x.WorkTypeCodeMandatory).HasColumnName("work_type_code_mandatory");
        builder.Property(x => x.LogoPositionOnDocuments).HasColumnName("logo_position_on_documents");
        builder.Property(x => x.UseContractCancelReason).HasColumnName("use_contract_cancel_reason");
        builder.Property(x => x.DefaultResponseTimeHours).HasColumnName("default_response_time_hours").HasPrecision(18, 5);
        builder.Property(x => x.DefaultWarrantyDuration).HasColumnName("default_warranty_duration");
        builder.Property(x => x.ServiceInvoiceNos).HasColumnName("service_invoice_nos");
        builder.Property(x => x.ContractInvoiceNos).HasColumnName("contract_invoice_nos");
        builder.Property(x => x.ServiceItemNos).HasColumnName("service_item_nos");
        builder.Property(x => x.ServiceOrderNos).HasColumnName("service_order_nos");
        builder.Property(x => x.ServiceContractNos).HasColumnName("service_contract_nos");
        builder.Property(x => x.ContractTemplateNos).HasColumnName("contract_template_nos");
        builder.Property(x => x.TroubleshootingNos).HasColumnName("troubleshooting_nos");
        builder.Property(x => x.PrepaidPostingDocumentNos).HasColumnName("prepaid_posting_document_nos");
        builder.Property(x => x.LoanerNos).HasColumnName("loaner_nos");
        builder.Property(x => x.ServJobResponsibilityCode).HasColumnName("serv_job_responsibility_code");
        builder.Property(x => x.ContractValueCalcMethod).HasColumnName("contract_value_calc_method");
        builder.Property(x => x.ContractValue).HasColumnName("contract_value").HasPrecision(18, 5);
        builder.Property(x => x.ServiceQuoteNos).HasColumnName("service_quote_nos");
        builder.Property(x => x.PostedServiceInvoiceNos).HasColumnName("posted_service_invoice_nos");
        builder.Property(x => x.PostedServCreditMemoNos).HasColumnName("posted_serv_credit_memo_nos");
        builder.Property(x => x.PostedServiceShipmentNos).HasColumnName("posted_service_shipment_nos");
        builder.Property(x => x.ShipmentOnInvoice).HasColumnName("shipment_on_invoice");
        builder.Property(x => x.CopyCommentsOrderToInvoice).HasColumnName("copy_comments_order_to_invoice");
        builder.Property(x => x.CopyCommentsOrderToShpt).HasColumnName("copy_comments_order_to_shpt");
        builder.Property(x => x.ServiceCreditMemoNos).HasColumnName("service_credit_memo_nos");
        builder.Property(x => x.CopyTimeSheetToOrder).HasColumnName("copy_time_sheet_to_order");
        builder.Property(x => x.BaseCalendarCode).HasColumnName("base_calendar_code");
        builder.Property(x => x.ContractCreditMemoNos).HasColumnName("contract_credit_memo_nos");
    }
}
