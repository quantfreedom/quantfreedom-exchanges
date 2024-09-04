namespace mufex.net.api.Models;

public class WalletFundType
{
    /*
    Deposit
    Withdraw
    RealisedPNL
    Commission
    Refund
    */
    private WalletFundType(string value)
    {
        Value = value;
    }

    public static WalletFundType Deposit
    {
        get => new("Deposit");
    }
    public static WalletFundType Withdraw
    {
        get => new("Withdraw");
    }
    public static WalletFundType RealisedPNL
    {
        get => new("RealisedPNL");
    }
    public static WalletFundType Commission
    {
        get => new("Commission");
    }
    public static WalletFundType Refund
    {
        get => new("Refund");
    }
    public string Value
    {
        get; private set;
    }
    public static implicit operator string(WalletFundType enm) => enm.Value;
    public override string ToString() => Value.ToString();
}
