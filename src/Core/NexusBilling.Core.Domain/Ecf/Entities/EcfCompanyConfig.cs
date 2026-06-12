using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Ecf.Enums;

namespace NexusBilling.Core.Domain.Ecf.Entities;

public class EcfCompanyConfig : Entity
{
    private EcfCompanyConfig()
    {
        Rnc = string.Empty;
        RepresentativeName = string.Empty;
        P12Path = string.Empty;
        P12PasswordHash = string.Empty;
        TenantId = null!;
    }

    private EcfCompanyConfig(
        TenantIdentifier tenantId,
        string rnc,
        string representativeName,
        EcfEnvironment environment,
        string p12Path,
        string p12PasswordHash)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        Rnc = rnc;
        RepresentativeName = representativeName;
        Environment = environment;
        P12Path = p12Path;
        P12PasswordHash = p12PasswordHash;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public TenantIdentifier TenantId { get; private set; }
    public string Rnc { get; private set; }
    public string RepresentativeName { get; private set; }
    public EcfEnvironment Environment { get; private set; }
    public string P12Path { get; private set; }
    public string P12PasswordHash { get; private set; }
    public bool IsActive { get; private set; }

    public static OperationResult<EcfCompanyConfig, DomainError> Create(
        TenantIdentifier tenantId,
        string rnc,
        string representativeName,
        EcfEnvironment environment,
        string p12Path,
        string p12PasswordHash)
    {
        if (tenantId.Value == Guid.Empty)
            return OperationResult<EcfCompanyConfig, DomainError>.Fail(DomainError.Validation("ecf_config.tenant_required", "El TenantIdentifier es obligatorio."));
        
        if (string.IsNullOrWhiteSpace(rnc))
            return OperationResult<EcfCompanyConfig, DomainError>.Fail(DomainError.Validation("ecf_config.rnc_required", "El RNC es obligatorio."));

        return OperationResult<EcfCompanyConfig, DomainError>.Ok(new EcfCompanyConfig(
            tenantId,
            rnc.Trim(),
            representativeName.Trim(),
            environment,
            p12Path.Trim(),
            p12PasswordHash.Trim()));
    }

    public void Update(string representativeName, EcfEnvironment environment, string p12Path, string p12PasswordHash)
    {
        RepresentativeName = representativeName.Trim();
        Environment = environment;
        P12Path = p12Path.Trim();
        P12PasswordHash = p12PasswordHash.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate() { IsActive = false; UpdatedAt = DateTime.UtcNow; }
    public void Activate() { IsActive = true; UpdatedAt = DateTime.UtcNow; }
}
