using Models.Exchange;
using Newtonsoft.Json;

namespace Models.Binance;

public class BinanceWsTradeData 
{
    [JsonProperty("E")]
    public string timestamp
    {
        get; set;
    }
    [JsonProperty("s")]
    public string symbol
    {
        get; set;
    }
    [JsonProperty("p")]
    public decimal price
    {
        get; set;
    }
    [JsonProperty("q")]
    public decimal assetQty
    {
        get; set;
    }
    [JsonProperty("m")]
    public bool side
    {
        get; set;
    }
}

/*
{
  "e": "aggTrade",  // Event type
  "E": 123456789,   // Event time
  "s": "BTCUSDT",    // Symbol
  "a": 5933014,     // Aggregate trade ID
  "p": "0.001",     // Price
  "q": "100",       // Quantity
  "f": 100,         // First trade ID
  "l": 105,         // Last trade ID
  "T": 123456785,   // Trade time
  "m": true,        // Is the buyer the market maker?
}*/