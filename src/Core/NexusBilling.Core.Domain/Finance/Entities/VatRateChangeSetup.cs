using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class VatRateChangeSetup : Entity
{
    private VatRateChangeSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public short UpdateGenProdPostGroups { get; private set; }
    public short UpdateGLAccounts { get; private set; }
    public short UpdateItems { get; private set; }
    public short UpdateItemTemplates { get; private set; }
    public short UpdateItemCharges { get; private set; }
    public short UpdateResources { get; private set; }
    public short UpdateGenJournalLines { get; private set; }
    public short UpdateGenJournalAllocation { get; private set; }
    public short UpdateStdGenJnlLines { get; private set; }
    public short UpdateResJournalLines { get; private set; }
    public short UpdateJobJournalLines { get; private set; }
    public short UpdateRequisitionLines { get; private set; }
    public short UpdateStdItemJnlLines { get; private set; }
    public short UpdateServiceDocs { get; private set; }
    public short UpdateServPriceAdjDetail { get; private set; }
    public short UpdateSalesDocuments { get; private set; }
    public short UpdatePurchaseDocuments { get; private set; }
    public short UpdateProductionOrders { get; private set; }
    public short UpdateWorkCenters { get; private set; }
    public short UpdateMachineCenters { get; private set; }
    public short UpdateReminders { get; private set; }
    public short UpdateFinanceChargeMemos { get; private set; }
    public bool VatRateChangeToolCompleted { get; private set; }
    public bool IgnoreStatusOnSalesDocs { get; private set; }
    public bool IgnoreStatusOnPurchDocs { get; private set; }
    public bool PerformConversion { get; private set; }
    public string ItemFilter { get; private set; }
    public string AccountFilter { get; private set; }
    public string ResourceFilter { get; private set; }

    public static OperationResult<VatRateChangeSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<VatRateChangeSetup, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new VatRateChangeSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<VatRateChangeSetup, DomainError>.Ok(entity);
    }
}
