using MediatR;
using NexusBilling.Core.Application.Finance.DTOs;

namespace NexusBilling.Core.Application.Finance.Features.GenJournalLines.Queries.GetGenJournalLines;

public record GetGenJournalLinesQuery(
    Guid TenantId,
    string JournalTemplateName,
    string JournalBatchName
) : IRequest<IReadOnlyList<GenJournalLineDto>>;
