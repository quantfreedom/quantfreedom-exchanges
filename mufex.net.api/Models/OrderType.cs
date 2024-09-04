namespace mufex.net.api.Models;

public struct OrderType
{

    private OrderType(string value)
    {
        Value = value;
    }

    public static OrderType Limit
    {
        get => new("Limit");
    }
    public static OrderType Market
    {
        get => new("Market");
    }
    public string Value
    {
        get; private set;
    }
    public static implicit operator string(OrderType enm) => enm.Value;
    public override readonly string ToString() => Value.ToString();
}
