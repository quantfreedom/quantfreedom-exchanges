namespace Models.Mufex;

public class MufexWalletFundType
{
    /*
    Deposit
    Withdraw
    RealisedPNL
    Commission
    Refund
    */
    private MufexWalletFundType(string value)
    {
        Value = value;
    }

    public static MufexWalletFundType Deposit
    {
        get => new("Deposit");
    }
    public static MufexWalletFundType Withdraw
    {
        get => new("Withdraw");
    }
    public static MufexWalletFundType RealisedPNL
    {
        get => new("RealisedPNL");
    }
    public static MufexWalletFundType Commission
    {
        get => new("Commission");
    }
    public static MufexWalletFundType Refund
    {
        get => new("Refund");
    }
    public string Value
    {
        get; private set;
    }
    public static implicit operator string(MufexWalletFundType enm) => enm.Value;
    public override string ToString() => Value.ToString();
}
