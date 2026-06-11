using System;

namespace NexusBilling.Core.Domain.Common.Errors;

public abstract record DomainError(string Code, string Message)
{
    public override string ToString() => $"[{Code}] {Message}";

    public static DomainError Validation(string code, string? message = null)
        => new GenericDomainError(code, message ?? code);

    public static DomainError Business(string code, string? message = null)
        => new GenericDomainError(code, message ?? code);

    public static DomainError NotFound(string code, string? message = null)
        => new GenericDomainError(code, message ?? code);

    private sealed record GenericDomainError(string InnerCode, string InnerMessage)
        : DomainError(InnerCode, InnerMessage);
}
