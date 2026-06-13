using MediatR;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Application.Finance.Features.GenJournalLines.Commands.PostGenJournalLine;

internal sealed class PostGenJournalLineCommandHandler(
    IGenJournalLineRepository lineRepo,
    IGLEntryRepository glEntryRepo,
    IUnitOfWork uow)
    : IRequestHandler<PostGenJournalLineCommand, OperationResult<int, DomainError>>
{
    public async Task<OperationResult<int, DomainError>> Handle(PostGenJournalLineCommand request, CancellationToken cancellationToken)
    {
        var lines = await lineRepo.GetLinesAsync(request.TenantId, request.JournalTemplateName, request.JournalBatchName, cancellationToken);
        
        if (!lines.Any())
            return OperationResult<int, DomainError>.Fail(DomainError.Validation("finance.empty_batch", "El diario no tiene líneas para registrar."));

        // Validate Balance
        var balance = lines.Sum(x => x.Amount);
        if (balance != 0)
            return OperationResult<int, DomainError>.Fail(DomainError.Validation("finance.batch_out_of_balance", "El diario está descuadrado."));

        // Find max Entry No
        // Instead of querying max entry no for each line, let's load it once and increment
        var existingEntries = await glEntryRepo.ListAsync(request.TenantId, null, 1, 1, cancellationToken);
        var currentEntryNo = existingEntries.Entries.FirstOrDefault()?.EntryNo ?? 0;

        int count = 0;
        foreach (var line in lines)
        {
            if (line.Amount != 0)
            {
                currentEntryNo++;
                var glEntryResult = GLEntry.Create(
                    TenantIdentifier.Create(request.TenantId),
                    currentEntryNo,
                    line.AccountNo ?? "",
                    line.PostingDate, // 4
                    (short)line.DocumentType, // 5
                    line.DocumentNo ?? "", // 6
                    line.Description ?? "", // 7
                    line.BalAccountNo ?? "", // 8
                    line.Amount, // 9
                    "", // 10 globalDimension1Code
                    "", // 11 globalDimension2Code
                    "SYSTEM", // 12 userId
                    line.SourceCode ?? "", // 13 sourceCode
                    line.SystemCreatedEntry, // 14
                    false, // 15 priorYearEntry
                    null, // 16 jobNo
                    line.Quantity, // 17 quantity
                    0, // 18 vatAmount
                    "", // 19 businessUnitCode
                    line.JournalBatchName ?? "", // 20
                    line.ReasonCode ?? "", // 21 reasonCode
                    0, // 22 genPostingType
                    line.GenBusPostingGroup ?? "", // 23
                    line.GenProdPostingGroup ?? "", // 24
                    0, // 25 balAccountType
                    0, // 26 transactionNo
                    line.DebitAmount, // 27
                    line.CreditAmount, // 28
                    null, // 29 documentDate
                    "", // 30 externalDocumentNo
                    0, // 31 sourceType
                    "", // 32 sourceNo
                    "", // 33 noSeries
                    "", // 34 taxAreaCode
                    false, // 35 taxLiable
                    "", // 36 taxGroupCode
                    false, // 37 useTax
                    "", // 38 vatBusPostingGroup
                    "", // 39 vatProdPostingGroup
                    0, // 40 additionalCurrencyAmount
                    0, // 41 addCurrencyDebitAmount
                    0, // 42 addCurrencyCreditAmount
                    0, // 43 closeIncomeStatementDimId
                    "", // 44 icPartnerCode
                    false, // 45 reversed
                    0, // 46 reversedByEntryNo
                    0, // 47 reversedEntryNo
                    0, // 48 dimensionSetId
                    "", // 49 prodOrderNo
                    0, // 50 faEntryType
                    0 // 51 faEntryNo
                );

                if (!glEntryResult.IsSuccess)
                    return OperationResult<int, DomainError>.Fail(glEntryResult.GetError()!);

                await glEntryRepo.AddAsync(glEntryResult.GetValue()!);
                count++;
            }

            // If there's a Balancing Account, we must create an entry for it.
            if (!string.IsNullOrWhiteSpace(line.BalAccountNo) && line.Amount != 0)
            {
                currentEntryNo++;
                var balGlEntryResult = GLEntry.Create(
                    TenantIdentifier.Create(request.TenantId),
                    currentEntryNo,
                    line.BalAccountNo ?? "",
                    line.PostingDate,
                    (short)line.DocumentType,
                    line.DocumentNo ?? "",
                    line.Description ?? "",
                    line.AccountNo ?? "",
                    -line.Amount, // amount
                    "", "", "SYSTEM", line.SourceCode ?? "", line.SystemCreatedEntry,
                    false, null, line.Quantity, 0, "", line.JournalBatchName ?? "", line.ReasonCode ?? "",
                    0, line.BalGenBusPostingGroup ?? "", line.BalGenProdPostingGroup ?? "", 0, 0,
                    line.CreditAmount, // Debit for Bal is Credit for Original
                    line.DebitAmount,  // Credit for Bal is Debit for Original
                    null, "", 0, "", "", "", false, "", false, "", "",
                    0, 0, 0, 0, "", false, 0, 0, 0, "", 0, 0
                );

                if (!balGlEntryResult.IsSuccess)
                    return OperationResult<int, DomainError>.Fail(balGlEntryResult.GetError()!);

                await glEntryRepo.AddAsync(balGlEntryResult.GetValue()!);
                count++;
            }

            // Finally, delete the journal line
            await lineRepo.DeleteAsync(line);
        }

        await uow.SaveChangesAsync(cancellationToken);

        return OperationResult<int, DomainError>.Ok(count);
    }
}
