using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class NoSeriesLine : Entity
{
    private NoSeriesLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string SeriesCode { get; set; } = string.Empty;
    public int LineNo { get; set; }
    public DateTime? StartingDate { get; set; }
    public string StartingNo { get; set; } = string.Empty;
    public string EndingNo { get; set; } = string.Empty;
    public string WarningNo { get; set; } = string.Empty;
    public int IncrementByNo { get; set; } = 1;
    public string LastNoUsed { get; set; } = string.Empty;
    public bool Open { get; set; } = true;
    public DateTime? LastDateUsed { get; set; }

    public static OperationResult<NoSeriesLine, DomainError> Create(
        TenantIdentifier tenantId,
        string seriesCode,
        string startingNo,
        string endingNo)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<NoSeriesLine, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        if (string.IsNullOrWhiteSpace(seriesCode))
            return OperationResult<NoSeriesLine, DomainError>.Fail(DomainError.Validation("no_series_line.code_required", "El código de serie es obligatorio."));

        if (string.IsNullOrWhiteSpace(startingNo))
            return OperationResult<NoSeriesLine, DomainError>.Fail(DomainError.Validation("no_series_line.starting_no_required", "El número inicial es obligatorio."));

        var entity = new NoSeriesLine
        {
            Id = Guid.NewGuid(),
            TenantId = tenantId,
            SeriesCode = seriesCode.Trim().ToUpperInvariant(),
            LineNo = 10000,
            StartingNo = startingNo.Trim(),
            EndingNo = endingNo?.Trim() ?? string.Empty,
            WarningNo = string.Empty,
            IncrementByNo = 1,
            LastNoUsed = string.Empty,
            Open = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        return OperationResult<NoSeriesLine, DomainError>.Ok(entity);
    }
}
