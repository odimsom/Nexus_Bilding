using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceMgtSetup : Entity
{
    private ServiceMgtSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public short FaultReportingLevel { get; private set; }
    public bool LinkServiceToServiceItem { get; private set; }
    public bool SalespersonMandatory { get; private set; }
    public decimal WarrantyDiscParts { get; private set; }
    public decimal WarrantyDiscLabor { get; private set; }
    public bool ContractRspTimeMandatory { get; private set; }
    public string ServiceOrderStartingFee { get; private set; }
    public bool RegisterContractChanges { get; private set; }
    public string ContractInvLineTextCode { get; private set; }
    public string ContractLineInvTextCode { get; private set; }
    public string ContractInvPeriodTextCode { get; private set; }
    public string ContractCreditLineTextCode { get; private set; }
    public string SendFirstWarningTo { get; private set; }
    public string SendSecondWarningTo { get; private set; }
    public string SendThirdWarningTo { get; private set; }
    public decimal FirstWarningWithinHours { get; private set; }
    public decimal SecondWarningWithinHours { get; private set; }
    public decimal ThirdWarningWithinHours { get; private set; }
    public short NextServiceCalcMethod { get; private set; }
    public bool ServiceOrderTypeMandatory { get; private set; }
    public short ServiceZonesOption { get; private set; }
    public bool ServiceOrderStartMandatory { get; private set; }
    public bool ServiceOrderFinishMandatory { get; private set; }
    public short ResourceSkillsOption { get; private set; }
    public bool OneServiceItemLineOrder { get; private set; }
    public bool UnitOfMeasureMandatory { get; private set; }
    public bool FaultReasonCodeMandatory { get; private set; }
    public int ContractServOrdMaxDays { get; private set; }
    public DateTime? LastContractServiceDate { get; private set; }
    public bool WorkTypeCodeMandatory { get; private set; }
    public short LogoPositionOnDocuments { get; private set; }
    public bool UseContractCancelReason { get; private set; }
    public decimal DefaultResponseTimeHours { get; private set; }
    public string DefaultWarrantyDuration { get; private set; }
    public string ServiceInvoiceNos { get; private set; }
    public string ContractInvoiceNos { get; private set; }
    public string ServiceItemNos { get; private set; }
    public string ServiceOrderNos { get; private set; }
    public string ServiceContractNos { get; private set; }
    public string ContractTemplateNos { get; private set; }
    public string TroubleshootingNos { get; private set; }
    public string PrepaidPostingDocumentNos { get; private set; }
    public string LoanerNos { get; private set; }
    public string ServJobResponsibilityCode { get; private set; }
    public short ContractValueCalcMethod { get; private set; }
    public decimal ContractValue { get; private set; }
    public string ServiceQuoteNos { get; private set; }
    public string PostedServiceInvoiceNos { get; private set; }
    public string PostedServCreditMemoNos { get; private set; }
    public string PostedServiceShipmentNos { get; private set; }
    public bool ShipmentOnInvoice { get; private set; }
    public bool CopyCommentsOrderToInvoice { get; private set; }
    public bool CopyCommentsOrderToShpt { get; private set; }
    public string ServiceCreditMemoNos { get; private set; }
    public bool CopyTimeSheetToOrder { get; private set; }
    public string BaseCalendarCode { get; private set; }
    public string ContractCreditMemoNos { get; private set; }

    public static OperationResult<ServiceMgtSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServiceMgtSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServiceMgtSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<ServiceMgtSetup, DomainError>.Ok(entity);
    }
}
