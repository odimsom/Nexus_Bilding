using MediatR;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;

namespace NexusBilling.Core.Application.Finance.Features.GenJournalLines.Commands.DeleteGenJournalLine;

public record DeleteGenJournalLineCommand(Guid Id) : IRequest<OperationResult<MediatR.Unit, DomainError>>;
