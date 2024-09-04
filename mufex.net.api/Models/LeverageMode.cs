
namespace mufex.net.api.Models;

public class LeverageMode
{
    public int Value
    {
        get; private set;
    }

    public static readonly LeverageMode CrossMargin = new(0);
    public static readonly LeverageMode IsolatedMargin = new(1);

    private LeverageMode(int value)
    {
        Value = value;
    }
}
