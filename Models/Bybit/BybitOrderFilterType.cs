namespace Models.Bybit;

public class BybitOrderFilterType
{

    private BybitOrderFilterType(string value)
    {
        Value = value;
    }

    public static BybitOrderFilterType LimitOrder
    {
        get => new("Order");
    }
    public static BybitOrderFilterType StopOrder
    {
        get => new("StopOrder");
    }
    public string Value
    {
        get; private set;
    }
    public static implicit operator string(BybitOrderFilterType enm) => enm.Value;
    public override string ToString() => Value.ToString();
}
