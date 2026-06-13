namespace NexusBilling.Core.Application.Finance.DTOs;

public record GLEntryDto(
    int EntryNo,
    string GLAccountNo,
    string GLAccountName,
    DateTime PostingDate,
    string DocumentType,
    string DocumentNo,
    string Description,
    decimal Amount,
    string SourceCode
);
