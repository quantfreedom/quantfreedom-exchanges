namespace mufex.net.api.Models;

public class OrderFilterType
{

    private OrderFilterType(string value)
    {
        Value = value;
    }

    public static OrderFilterType conditional
    {
        get => new("conditional");
    }
    public static OrderFilterType active
    {
        get => new("active");
    }
    public string Value
    {
        get; private set;
    }
    public static implicit operator string(OrderFilterType enm) => enm.Value;
    public override string ToString() => Value.ToString();
}
