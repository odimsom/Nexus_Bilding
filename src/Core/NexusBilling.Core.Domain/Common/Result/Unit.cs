namespace NexusBilling.Core.Domain.Common.Result;

public readonly struct Unit : IEquatable<Unit>
{
    private static readonly Unit _value = new();
    public static ref readonly Unit Value => ref _value;

    public bool Equals(Unit other) => true;
    public override bool Equals(object? obj) => obj is Unit;
    public override int GetHashCode() => 0;

    public static bool operator ==(Unit left, Unit right) => true;
    public static bool operator !=(Unit left, Unit right) => false;
}
