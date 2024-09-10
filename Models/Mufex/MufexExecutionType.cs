using System;

namespace Models.Mufex;

public class MufexExecutionType
{

    private MufexExecutionType(string value)
    {
        Value = value;
    }

    public static MufexExecutionType Trade
    {
        get => new("Trade");
    }
    public static MufexExecutionType AdlTrade
    {
        get => new("AdlTrade");
    }
    public static MufexExecutionType Funding
    {
        get => new("Funding");
    }
    public static MufexExecutionType BustTrade
    {
        get => new("BustTrade");
    }
    public string Value
    {
        get; private set;
    }
    public static implicit operator string(MufexExecutionType enm) => enm.Value;
    public override string ToString() => Value.ToString();
}


