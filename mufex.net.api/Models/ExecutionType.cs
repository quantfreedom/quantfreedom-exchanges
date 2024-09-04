using System;

namespace mufex.net.api.Models;

public class ExecutionType
{

    private ExecutionType(string value)
    {
        Value = value;
    }

    public static ExecutionType Trade
    {
        get => new("Trade");
    }
    public static ExecutionType AdlTrade
    {
        get => new("AdlTrade");
    }
    public static ExecutionType Funding
    {
        get => new("Funding");
    }
    public static ExecutionType BustTrade
    {
        get => new("BustTrade");
    }
    public string Value
    {
        get; private set;
    }
    public static implicit operator string(ExecutionType enm) => enm.Value;
    public override string ToString() => Value.ToString();
}


