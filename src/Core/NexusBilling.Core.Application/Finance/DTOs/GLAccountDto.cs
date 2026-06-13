namespace NexusBilling.Core.Application.Finance.DTOs;

public record GLAccountDto(
    string No,
    string Name,
    short AccountType,
    short IncomeBalance,
    bool Blocked,
    decimal Balance = 0m
);
