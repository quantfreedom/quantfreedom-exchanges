namespace Models.Mufex;

public class MufexOrderFilterType
{

    private MufexOrderFilterType(string value)
    {
        Value = value;
    }

    public static MufexOrderFilterType conditional
    {
        get => new("conditional");
    }
    public static MufexOrderFilterType active
    {
        get => new("active");
    }
    public string Value
    {
        get; private set;
    }
    public static implicit operator string(MufexOrderFilterType enm) => enm.Value;
    public override string ToString() => Value.ToString();
}
