using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class MarketingSetup : Entity
{
    private MarketingSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string ContactNos { get; private set; }
    public string CampaignNos { get; private set; }
    public string SegmentNos { get; private set; }
    public string ToDoNos { get; private set; }
    public string OpportunityNos { get; private set; }
    public string BusRelCodeForCustomers { get; private set; }
    public string BusRelCodeForVendors { get; private set; }
    public string BusRelCodeForBankAccs { get; private set; }
    public bool InheritSalespersonCode { get; private set; }
    public bool InheritTerritoryCode { get; private set; }
    public bool InheritCountryRegionCode { get; private set; }
    public bool InheritLanguageCode { get; private set; }
    public bool InheritAddressDetails { get; private set; }
    public bool InheritCommunicationDetails { get; private set; }
    public string DefaultSalespersonCode { get; private set; }
    public string DefaultTerritoryCode { get; private set; }
    public string DefaultCountryRegionCode { get; private set; }
    public string DefaultLanguageCode { get; private set; }
    public string DefaultSalesCycleCode { get; private set; }
    public short AttachmentStorageType { get; private set; }
    public string AttachmentStorageLocation { get; private set; }
    public bool AutosearchForDuplicates { get; private set; }
    public int SearchHit { get; private set; }
    public bool MaintainDuplSearchStrings { get; private set; }
    public int MergefieldLanguageId { get; private set; }
    public string DefCompanySalutationCode { get; private set; }
    public string DefaultPersonSalutationCode { get; private set; }
    public short DefaultCorrespondenceType { get; private set; }
    public string QueueFolderPath { get; private set; }
    public byte[]? QueueFolderUid { get; private set; }
    public string StorageFolderPath { get; private set; }
    public byte[]? StorageFolderUid { get; private set; }
    public string DefaultToDoDateCalculation { get; private set; }
    public string AutodiscoveryEMailAddress { get; private set; }
    public int EmailBatchSize { get; private set; }
    public string ExchangeServiceUrl { get; private set; }
    public string ExchangeAccountUserName { get; private set; }
    public Guid ExchangeAccountPasswordKey { get; private set; }

    public static OperationResult<MarketingSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<MarketingSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new MarketingSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<MarketingSetup, DomainError>.Ok(entity);
    }
}
