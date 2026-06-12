using MediatR;

namespace NexusBilling.Core.Application.Administration.Commands;

public record UpsertNoSeriesCommand(
    Guid TenantId,
    string Code,
    string Description,
    bool DefaultNos,
    bool ManualNos) : IRequest<bool>;

public record UpsertNoSeriesLineCommand(
    Guid TenantId,
    string SeriesCode,
    string StartingNo,
    string EndingNo,
    int IncrementByNo) : IRequest<bool>;

public record DeleteNoSeriesCommand(Guid TenantId, string Code) : IRequest<bool>;
