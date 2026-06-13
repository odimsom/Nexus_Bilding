using MediatR;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;

namespace NexusBilling.Core.Application.Finance.Features.GenJournalLines.Commands.PostGenJournalLine;

public record PostGenJournalLineCommand(
    Guid TenantId,
    string JournalTemplateName,
    string JournalBatchName
) : IRequest<OperationResult<int, DomainError>>;
