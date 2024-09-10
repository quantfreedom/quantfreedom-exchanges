namespace Models.Mufex;

public struct MufexOrderType
{

    private MufexOrderType(string value)
    {
        Value = value;
    }

    public static MufexOrderType Limit
    {
        get => new("Limit");
    }
    public static MufexOrderType Market
    {
        get => new("Market");
    }
    public string Value
    {
        get; private set;
    }
    public static implicit operator string(MufexOrderType enm) => enm.Value;
    public override readonly string ToString() => Value.ToString();
}
