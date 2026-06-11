using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class FinanceChargeMemoLine : Entity
{
    private FinanceChargeMemoLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string FinanceChargeMemoNo { get; private set; }
    public int LineNo { get; private set; }
    public int AttachedToLineNo { get; private set; }
    public short Type { get; private set; }
    public int EntryNo { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public DateTime? DueDate { get; private set; }
    public short DocumentType { get; private set; }
    public string DocumentNo { get; private set; }
    public string Description { get; private set; }
    public decimal OriginalAmount { get; private set; }
    public decimal RemainingAmount { get; private set; }
    public string No { get; private set; }
    public decimal Amount { get; private set; }
    public decimal InterestRate { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public decimal Vat { get; private set; }
    public short VatCalculationType { get; private set; }
    public decimal VatAmount { get; private set; }
    public string TaxGroupCode { get; private set; }
    public string VatProdPostingGroup { get; private set; }
    public string VatIdentifier { get; private set; }
    public short LineType { get; private set; }
    public string VatClauseCode { get; private set; }
    public bool SystemCreatedEntry { get; private set; }

    public static OperationResult<FinanceChargeMemoLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FinanceChargeMemoLine, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FinanceChargeMemoLine()
        {
            TenantId = tenantId
        };
        return OperationResult<FinanceChargeMemoLine, DomainError>.Ok(entity);
    }
}
