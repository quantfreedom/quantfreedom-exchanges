using Newtonsoft.Json;

namespace Exchanges.Mufex.Models;
public class GetWalletBalanceData
{
    [JsonProperty("list")]
    public List<GetWalletBalanceDataList>? list { get; set; }
}

public class GetWalletBalanceDataList
{
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

/*
https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-balance
{
    "code": 0,
        "message": "OK",
        "data":  {
        "list": [
            {
                "coin": "BTC",
                "equity": "0.80319649",
                "walletBalance": "0.80319649",
                "positionMargin": "0",
                "availableBalance": "0.80319649",
                "orderMargin": "0",
                "occClosingFee": "0",
                "occFundingFee": "0",
                "unrealisedPnl": "0",
                "cumRealisedPnl": "0.00120039",
                "givenCash": "0",
                "serviceCash": "0"
            }
        ]
    },
    "ext_info": {},
    "time": 1658736635763
}
*/  