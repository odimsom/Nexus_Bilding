using System;

namespace NexusBilling.Core.Domain.Common.Result;

public class OperationResult<TResult, TError>
{
    private readonly TResult? _value;
    private readonly TError? _error;

    public bool IsSuccess { get; }

    private OperationResult(bool isSuccess, TResult? value, TError? error)
    {
        IsSuccess = isSuccess;
        _value = value;
        _error = error;
    }

    public static OperationResult<TResult, TError> Ok(TResult? value) =>
        new(true, value, default);

    public static OperationResult<TResult, TError> Fail(TError? error) =>
        new(false, default, error);

    public TResult GetValue() => IsSuccess
        ? _value!
        : throw new InvalidOperationException($"Operation failed. Error: {_error}");

    public TError GetError() => !IsSuccess
        ? _error!
        : throw new InvalidOperationException("Cannot access error of a successful operation.");

    public TOut Match<TOut>(
        Func<TResult?, TOut> onOk,
        Func<TError?, TOut> onFail) =>
        IsSuccess ? onOk(_value) : onFail(_error);
}
