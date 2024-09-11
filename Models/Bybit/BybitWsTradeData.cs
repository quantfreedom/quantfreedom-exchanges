

using Models.Exchange;
using Newtonsoft.Json;

namespace Models.Bybit;
public class BybitWsTradeDataList
{

    [JsonProperty("topic")]
    public string Topic
    {
        get; set;
    }

    [JsonProperty("type")]
    public string Type
    {
        get; set;
    }

    [JsonProperty("ts")]
    public long Ts
    {
        get; set;
    }

    [JsonProperty("data")]
    public List<BybitWsTradeData> TradeList
    {
        get; set;
    }
}


public class BybitWsTradeData
{
    [JsonProperty("T")]
    public long Timestamp
    {
        get; set;
    }

    [JsonProperty("s")]
    public string Symbol
    {
        get; set;
    }

    [JsonProperty("S")]
    public string Side
    {
        get; set;
    }

    [JsonProperty("v")]
    public decimal AssetQty
    {
        get; set;
    }

    [JsonProperty("p")]
    public decimal Price
    {
        get; set;
    }
}

/*
https://bybit-exchange.github.io/docs/v5/websocket/public/trade
{
    "topic": "publicTrade.BTCUSDT",
    "type": "snapshot",
    "ts": 1672304486868,
    "data": [
        {
            "T": 1672304486865,
            "s": "BTCUSDT",
            "S": "Buy",
            "v": "0.001",
            "p": "16578.50",
            "L": "PlusTick",
            "i": "20f43950-d8dd-5b31-9112-a178eb6023af",
            "BT": false
        }
    ]
}
*/