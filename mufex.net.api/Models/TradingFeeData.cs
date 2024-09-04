using Newtonsoft.Json;

namespace mufex.net.api.Models;

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
