

using Newtonsoft.Json;

namespace Models.Exchange;

public class ExchangeWsTradeDataParsed
{
    public long Timestamp
    {
        get; set;
    }
    public decimal AssetQty
    {
        get; set;
    }
    public decimal UsdtQty
    {
        get; set;
    }
    public decimal Price
    {
        get; set;
    }
    public string Side
    {
        get; set;
    }
}

public class ExchangeWsTradeData
{
    [JsonProperty("T")]
    public long timestamp
    {
        get; set;
    }

    [JsonProperty("s")]
    public string symbol
    {
        get; set;
    }

    [JsonProperty("S")]
    public string side
    {
        get; set;
    }

    [JsonProperty("v")]
    public decimal assetQty
    {
        get; set;
    }

    [JsonProperty("p")]
    public decimal price
    {
        get; set;
    }
}
