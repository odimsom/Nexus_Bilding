using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;

namespace NexusBilling.Core.Domain.Ecf.Entities;

public class EcfNcfSequence : Entity
{
    private EcfNcfSequence()
    {
        NcfType = string.Empty;
        TenantId = null!;
    }

    private EcfNcfSequence(
        TenantIdentifier tenantId,
        string ncfType,
        long currentNumber,
        long maxNumber,
        DateTime expirationDate)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        NcfType = ncfType;
        CurrentNumber = currentNumber;
        MaxNumber = maxNumber;
        ExpirationDate = expirationDate;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public TenantIdentifier TenantId { get; private set; }
    public string NcfType { get; private set; }
    public long CurrentNumber { get; private set; }
    public long MaxNumber { get; private set; }
    public DateTime ExpirationDate { get; private set; }

    public static OperationResult<EcfNcfSequence, DomainError> Create(
        TenantIdentifier tenantId,
        string ncfType,
        long currentNumber,
        long maxNumber,
        DateTime expirationDate)
    {
        if (tenantId.Value == Guid.Empty)
            return OperationResult<EcfNcfSequence, DomainError>.Fail(DomainError.Validation("ecf_sequence.tenant_required", "El TenantIdentifier es obligatorio."));
        
        if (string.IsNullOrWhiteSpace(ncfType))
            return OperationResult<EcfNcfSequence, DomainError>.Fail(DomainError.Validation("ecf_sequence.ncf_type_required", "El tipo de NCF es obligatorio."));

        if (maxNumber < currentNumber)
            return OperationResult<EcfNcfSequence, DomainError>.Fail(DomainError.Validation("ecf_sequence.invalid_range", "El número máximo no puede ser menor al actual."));

        return OperationResult<EcfNcfSequence, DomainError>.Ok(new EcfNcfSequence(
            tenantId,
            ncfType.Trim().ToUpper(),
            currentNumber,
            maxNumber,
            expirationDate));
    }

    public OperationResult<string, DomainError> ConsumeNext()
    {
        if (CurrentNumber >= MaxNumber)
            return OperationResult<string, DomainError>.Fail(DomainError.Validation("ecf_sequence.exhausted", $"La secuencia {NcfType} se ha agotado."));

        if (DateTime.UtcNow > ExpirationDate)
            return OperationResult<string, DomainError>.Fail(DomainError.Validation("ecf_sequence.expired", $"La secuencia {NcfType} ha expirado."));

        CurrentNumber++;
        UpdatedAt = DateTime.UtcNow;
        
        // Formato E-CF: Letra E + Tipo (2) + Secuencia (10 ceros a la izquierda)
        // Ejemplo: E310000000047
        string formattedNcf = $"E{NcfType.Substring(1)}{CurrentNumber:D10}";
        return OperationResult<string, DomainError>.Ok(formattedNcf);
    }
}
