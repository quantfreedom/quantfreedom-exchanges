

using Newtonsoft.Json;

namespace Models.Exchange;

public class ExchangeWsTradeDataParsed
{
    public string Exchange
    {
        get; set;
    }
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
    [JsonProperty("timestamp")]
    public long timestamp
    {
        get; set;
    }

    [JsonProperty("symbol")]
    public string symbol
    {
        get; set;
    }

    [JsonProperty("side")]
    public string side
    {
        get; set;
    }

    [JsonProperty("assetQty")]
    public decimal assetQty
    {
        get; set;
    }

    [JsonProperty("price")]
    public decimal price
    {
        get; set;
    }
}
