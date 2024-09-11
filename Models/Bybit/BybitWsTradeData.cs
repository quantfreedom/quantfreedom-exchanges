

using Models.Exchange;
using Newtonsoft.Json;

namespace Models.Bybit;
public class BybitWsTradeDataList
{

    [JsonProperty("topic")]
    public string topic
    {
        get; set;
    }

    [JsonProperty("type")]
    public string type
    {
        get; set;
    }

    [JsonProperty("ts")]
    public long ts
    {
        get; set;
    }

    [JsonProperty("data")]
    public List<BybitWsTradeData> tradeList
    {
        get; set;
    }
}


public class BybitWsTradeData
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




/*
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