using MediatR;
using NexusBilling.Core.Domain.Administration.Repositories;

namespace NexusBilling.Core.Application.Administration.Features.NoSeries.Queries.GetNoSeries;

public sealed class GetNoSeriesQueryHandler(INoSeriesRepository repo, INoSeriesLineRepository lineRepo)
    : IRequestHandler<GetNoSeriesQuery, IReadOnlyList<NoSeriesDto>>
{
    public async Task<IReadOnlyList<NoSeriesDto>> Handle(GetNoSeriesQuery q, CancellationToken ct)
    {
        var series = await repo.GetAllForTenantAsync(q.TenantId, ct);
        var result = new List<NoSeriesDto>();
        foreach (var s in series)
        {
            var activeLine = await lineRepo.GetActiveLineAsync(q.TenantId, s.Code, ct);
            result.Add(new NoSeriesDto(
                s.Code, s.Description, s.DefaultNos, s.ManualNos,
                activeLine?.StartingNo,
                activeLine?.EndingNo,
                activeLine?.LastNoUsed,
                activeLine?.IncrementByNo ?? 1,
                activeLine?.Open ?? false));
        }
        return result;
    }
}
