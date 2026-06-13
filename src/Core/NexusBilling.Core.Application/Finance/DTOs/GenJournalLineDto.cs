namespace NexusBilling.Core.Application.Finance.DTOs;

public record GenJournalLineDto(
    Guid Id,
    string JournalTemplateName,
    string JournalBatchName,
    int LineNo,
    string AccountNo,
    string? PostingDate,
    string DocumentNo,
    string Description,
    decimal Amount,
    string BalAccountNo
);
