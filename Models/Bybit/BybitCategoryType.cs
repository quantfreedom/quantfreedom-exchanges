namespace Models.Bybit;

public class BybitCategoryType
{

    private BybitCategoryType(string value)
    {
        Value = value;
    }

    public static BybitCategoryType Spot
    {
        get => new("spot");
    }
    public static BybitCategoryType Linear
    {
        get => new("linear");
    }

    public static BybitCategoryType Inverse
    {
        get => new("inverse");
    }

    public static BybitCategoryType Option
    {
        get => new("option");
    }
    public string Value
    {
        get; private set;
    }
    public static implicit operator string(BybitCategoryType enm) => enm.Value;
    public override string ToString() => Value.ToString();
}


