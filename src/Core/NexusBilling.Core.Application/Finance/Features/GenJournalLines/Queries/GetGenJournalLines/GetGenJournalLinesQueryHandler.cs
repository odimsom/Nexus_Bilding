using MediatR;
using NexusBilling.Core.Application.Finance.DTOs;
using NexusBilling.Core.Domain.Finance.Repositories;

namespace NexusBilling.Core.Application.Finance.Features.GenJournalLines.Queries.GetGenJournalLines;

internal sealed class GetGenJournalLinesQueryHandler(
    IGenJournalLineRepository lineRepo) 
    : IRequestHandler<GetGenJournalLinesQuery, IReadOnlyList<GenJournalLineDto>>
{
    public async Task<IReadOnlyList<GenJournalLineDto>> Handle(GetGenJournalLinesQuery request, CancellationToken cancellationToken)
    {
        var lines = await lineRepo.GetLinesAsync(request.TenantId, request.JournalTemplateName, request.JournalBatchName, cancellationToken);
        
        return lines.Select(x => new GenJournalLineDto(
            x.Id,
            x.JournalTemplateName,
            x.JournalBatchName,
            x.LineNo,
            x.AccountNo,
            x.PostingDate,
            x.DocumentNo,
            x.Description,
            x.Amount,
            x.BalAccountNo
        )).ToList();
    }
}
