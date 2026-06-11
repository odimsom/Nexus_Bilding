using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Errors;

public static class FinanceErrors
{
    public static DomainError AccountNotFound(string no) => 
        DomainError.NotFound("finance.account_not_found", $"La cuenta contable '{no}' no existe.");
        
    public static DomainError AccountAlreadyExists(string no) => 
        DomainError.Business("finance.account_already_exists", $"La cuenta contable '{no}' ya existe.");
}
