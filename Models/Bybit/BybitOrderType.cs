namespace Models.Bybit;

public struct BybitOrderType
{

    private BybitOrderType(string value)
    {
        Value = value;
    }

    public static BybitOrderType Limit
    {
        get => new("Limit");
    }
    public static BybitOrderType Market
    {
        get => new("Market");
    }
    public string Value
    {
        get; private set;
    }
    public static implicit operator string(BybitOrderType enm) => enm.Value;
    public override readonly string ToString() => Value.ToString();
}
