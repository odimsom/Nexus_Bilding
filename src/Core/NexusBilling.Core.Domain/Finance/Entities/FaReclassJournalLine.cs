using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class FaReclassJournalLine : Entity
{
    private FaReclassJournalLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JournalTemplateName { get; private set; }
    public string JournalBatchName { get; private set; }
    public int LineNo { get; private set; }
    public string FaNo { get; private set; }
    public string NewFaNo { get; private set; }
    public DateTime? FaPostingDate { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public string DepreciationBookCode { get; private set; }
    public decimal ReclassifyAcqCostAmount { get; private set; }
    public decimal ReclassifyAcqCost { get; private set; }
    public bool ReclassifyAcquisitionCost { get; private set; }
    public bool ReclassifyDepreciation { get; private set; }
    public bool ReclassifyWriteDown { get; private set; }
    public bool ReclassifyAppreciation { get; private set; }
    public bool ReclassifyCustom1 { get; private set; }
    public bool ReclassifyCustom2 { get; private set; }
    public bool ReclassifySalvageValue { get; private set; }
    public bool InsertBalAccount { get; private set; }
    public string Description { get; private set; }
    public string DocumentNo { get; private set; }
    public bool CalcDb1DeprAmount { get; private set; }

    public static OperationResult<FaReclassJournalLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FaReclassJournalLine, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FaReclassJournalLine()
        {
            TenantId = tenantId
        };
        return OperationResult<FaReclassJournalLine, DomainError>.Ok(entity);
    }
}
