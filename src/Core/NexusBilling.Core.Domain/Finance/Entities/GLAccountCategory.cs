using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class GLAccountCategory : Entity
{
    private GLAccountCategory() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public int ParentEntryNo { get; private set; }
    public int SiblingSequenceNo { get; private set; }
    public string PresentationOrder { get; private set; }
    public int Indentation { get; private set; }
    public string Description { get; private set; }
    public short AccountCategory { get; private set; }
    public short IncomeBalance { get; private set; }
    public short AdditionalReportDefinition { get; private set; }
    public bool SystemGenerated { get; private set; }

    public static OperationResult<GLAccountCategory, DomainError> Create(
        TenantIdentifier tenantId,
        int entryNo,
        int parentEntryNo,
        int siblingSequenceNo,
        string presentationOrder,
        int indentation,
        string description,
        short accountCategory,
        short incomeBalance,
        short additionalReportDefinition,
        bool systemGenerated)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<GLAccountCategory, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(presentationOrder))
            return OperationResult<GLAccountCategory, DomainError>.Fail(DomainError.Validation("finance.presentation_order_required", "El campo presentation_order es obligatorio."));
        if (string.IsNullOrWhiteSpace(description))
            return OperationResult<GLAccountCategory, DomainError>.Fail(DomainError.Validation("finance.description_required", "El campo description es obligatorio."));

        var entity = new GLAccountCategory()
        {
            TenantId = tenantId,
            EntryNo = entryNo,
            ParentEntryNo = parentEntryNo,
            SiblingSequenceNo = siblingSequenceNo,
            PresentationOrder = presentationOrder.Trim(),
            Indentation = indentation,
            Description = description.Trim(),
            AccountCategory = accountCategory,
            IncomeBalance = incomeBalance,
            AdditionalReportDefinition = additionalReportDefinition,
            SystemGenerated = systemGenerated,
        };

        return OperationResult<GLAccountCategory, DomainError>.Ok(entity);
    }
}
