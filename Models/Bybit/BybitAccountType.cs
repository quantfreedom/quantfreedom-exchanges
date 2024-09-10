namespace Models.Bybit;

public class BybitAccountType
{

    private BybitAccountType(string value)
    {
        Value = value;
    }

    public static BybitAccountType Unified
    {
        get => new("Unified");
    }
    public static BybitAccountType Funding
    {
        get => new("FUND");
    }
    public string Value
    {
        get; private set;
    }
    public static implicit operator string(BybitAccountType enm) => enm.Value;
    public override string ToString() => Value.ToString();
}
