using System.Text.RegularExpressions;
using NexusBilling.Core.Domain.Administration.Repositories;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Application.Administration.Services;

public class NoSeriesService(INoSeriesLineRepository lineRepo, IUnitOfWork uow)
{
    /// <summary>
    /// Generates and reserves the next document number for a given series code.
    /// Increments the trailing numeric digits. Example: "PV-000" → "PV-001".
    /// </summary>
    public async Task<string> GetNextNoAsync(Guid tenantId, string seriesCode, CancellationToken ct = default)
    {
        var line = await lineRepo.GetActiveLineAsync(tenantId, seriesCode, ct)
            ?? throw new InvalidOperationException($"No se encontró una línea activa para la serie '{seriesCode}'.");

        string nextNo = IncrementNo(
            string.IsNullOrEmpty(line.LastNoUsed) ? line.StartingNo : line.LastNoUsed,
            line.IncrementByNo);

        if (!string.IsNullOrEmpty(line.EndingNo) &&
            string.Compare(nextNo, line.EndingNo, StringComparison.Ordinal) > 0)
            throw new InvalidOperationException($"La serie '{seriesCode}' ha alcanzado su número final ({line.EndingNo}).");

        line.LastNoUsed = nextNo;
        line.LastDateUsed = DateTime.UtcNow;
        line.UpdatedAt = DateTime.UtcNow;
        await lineRepo.UpdateAsync(line, ct);
        await uow.SaveChangesAsync(ct);

        return nextNo;
    }

    private static string IncrementNo(string no, int increment)
    {
        // Find trailing digit sequence: "PV-000001" → prefix="PV-", digits="000001"
        var match = Regex.Match(no, @"^(.*?)(\d+)$");
        if (!match.Success) return no + increment.ToString();

        string prefix = match.Groups[1].Value;
        string digits = match.Groups[2].Value;
        long current = long.Parse(digits);
        long next = current + increment;
        string nextDigits = next.ToString().PadLeft(digits.Length, '0');
        return prefix + nextDigits;
    }
}
