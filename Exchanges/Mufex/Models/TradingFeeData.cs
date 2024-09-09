using Newtonsoft.Json;

namespace Exchanges.Mufex.Models;

public class TradingFeeData
{
    [JsonProperty("list")]
    public List<TradingFeeDataList>? list { get; set; }
}
public class TradingFeeDataList
{
    /// <summary>
    /// https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-balance
    /// </summary>
    public string? symbol { get; set; }
    public double? takerFeeRate { get; set; }
    public double? makerFeeRate { get; set; }
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