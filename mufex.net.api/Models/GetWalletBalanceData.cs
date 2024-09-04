using Newtonsoft.Json;

namespace mufex.net.api.Models;


public class GetWalletBalanceData
{
    /// <summary>
    /// https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-balance
    /// </summary>

    [JsonProperty("list")]
    public List<GetWalletBalanceDataList>? list { get; set; }
}

public class GetWalletBalanceDataList
{
    /// <summary>
    /// https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-balance
    /// </summary>
    public string? coin { get; set; }
    public double? equity { get; set; }
    public double? walletBalance { get; set; }
    public double? positionMargin { get; set; }
    public double? availableBalance { get; set; }
    public double? orderMargin { get; set; }
    public double? occClosingFee { get; set; }
    public double? occFundingFee { get; set; }
    public double? unrealisedPnl { get; set; }
    public double? cumRealisedPnl { get; set; }
    public double? givenCash { get; set; }
    public double? serviceCash { get; set; }
}

