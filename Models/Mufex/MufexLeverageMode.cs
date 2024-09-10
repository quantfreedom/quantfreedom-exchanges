
namespace Models.Mufex;

public class MufexLeverageMode
{
    public int Value
    {
        get; private set;
    }

    public static readonly MufexLeverageMode CrossMargin = new(0);
    public static readonly MufexLeverageMode IsolatedMargin = new(1);

    private MufexLeverageMode(int value)
    {
        Value = value;
    }
}
