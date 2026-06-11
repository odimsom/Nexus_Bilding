using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class Job : Entity
{
    private Job() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string SearchDescription { get; private set; }
    public string Description { get; private set; }
    public string Description2 { get; private set; }
    public string BillToCustomerNo { get; private set; }
    public DateTime? CreationDate { get; private set; }
    public DateTime? StartingDate { get; private set; }
    public DateTime? EndingDate { get; private set; }
    public short Status { get; private set; }
    public string PersonResponsible { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public string JobPostingGroup { get; private set; }
    public short Blocked { get; private set; }
    public DateTime? LastDateModified { get; private set; }
    public string CustomerDiscGroup { get; private set; }
    public string CustomerPriceGroup { get; private set; }
    public string LanguageCode { get; private set; }
    public byte[]? Picture { get; private set; }
    public string BillToName { get; private set; }
    public string BillToAddress { get; private set; }
    public string BillToAddress2 { get; private set; }
    public string BillToCity { get; private set; }
    public string BillToCounty { get; private set; }
    public string BillToPostCode { get; private set; }
    public string NoSeries { get; private set; }
    public string BillToCountryRegionCode { get; private set; }
    public string BillToName2 { get; private set; }
    public short Reserve { get; private set; }
    public Guid Image { get; private set; }
    public string WipMethod { get; private set; }
    public string CurrencyCode { get; private set; }
    public string BillToContactNo { get; private set; }
    public string BillToContact { get; private set; }
    public DateTime? WipPostingDate { get; private set; }
    public string InvoiceCurrencyCode { get; private set; }
    public short ExchCalculationCost { get; private set; }
    public short ExchCalculationPrice { get; private set; }
    public bool AllowScheduleContractLines { get; private set; }
    public bool Complete { get; private set; }
    public bool ApplyUsageLink { get; private set; }
    public short WipPostingMethod { get; private set; }
    public bool OverBudget { get; private set; }
    public string ProjectManager { get; private set; }

    public static OperationResult<Job, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<Job, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new Job()
        {
            TenantId = tenantId
        };
        return OperationResult<Job, DomainError>.Ok(entity);
    }
}
