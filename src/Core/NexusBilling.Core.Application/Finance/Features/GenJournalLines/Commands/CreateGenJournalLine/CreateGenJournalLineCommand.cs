using MediatR;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;

namespace NexusBilling.Core.Application.Finance.Features.GenJournalLines.Commands.CreateGenJournalLine;

public record CreateGenJournalLineCommand(
    Guid TenantId,
    string JournalTemplateName,
    string JournalBatchName,
    string AccountNo,
    string? PostingDate,
    string DocumentNo,
    string Description,
    decimal Amount,
    string BalAccountNo
) : IRequest<OperationResult<Guid, DomainError>>;
