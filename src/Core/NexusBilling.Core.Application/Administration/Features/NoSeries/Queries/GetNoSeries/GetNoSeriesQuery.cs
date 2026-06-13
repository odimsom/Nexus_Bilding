using MediatR;

namespace NexusBilling.Core.Application.Administration.Features.NoSeries.Queries.GetNoSeries;

public record NoSeriesDto(
    string Code,
    string Description,
    bool DefaultNos,
    bool ManualNos,
    string? StartingNo,
    string? EndingNo,
    string? LastNoUsed,
    int IncrementByNo,
    bool Open);

public record GetNoSeriesQuery(Guid TenantId) : IRequest<IReadOnlyList<NoSeriesDto>>;
