using Newtonsoft.Json;

namespace Exchanges.Mufex.Models;

public class GeneralResponse<T>
{
  // do I need to have this json property? TODO: check

  [JsonProperty("code")]
  public int? code { get; set; }

  [JsonProperty("message")]
  public string? message { get; set; }

  [JsonProperty("data")]
  public T? data { get; set; }

  [JsonProperty("ext_info")]
  public Dictionary<string, object>? ext_info { get; set; }

  [JsonProperty("time")]
  public long? time { get; set; }
}



public class BinanceAggTradeData
{
  [JsonProperty("e")]
  public string? eventType { get; set; }

  [JsonProperty("E")]
  public long? eventTime { get; set; }

  [JsonProperty("s")]
  public string? symbol { get; set; }

  [JsonProperty("a")]
  public long? aggregateTradeId { get; set; }

  [JsonProperty("p")]
  public double? price { get; set; }

  [JsonProperty("q")]
  public double? quantity { get; set; }

  [JsonProperty("f")]
  public long? firstTradeId { get; set; }

  [JsonProperty("l")]
  public long? lastTradeId { get; set; }

  [JsonProperty("T")]
  public long? tradeTime { get; set; }

  [JsonProperty("m")]
  public bool? isBuyerMaker { get; set; }
}

public class Root
{
  [JsonProperty("topic")]
  public string Topic { get; set; }

  [JsonProperty("type")]
  public string Type { get; set; }

  [JsonProperty("data")]
  public Data Data { get; set; }

  [JsonProperty("cs")]
  public long crossSequence { get; set; }

  [JsonProperty("ts")]
  public long timestamp { get; set; }
}

// Data object
public class Data
{
  [JsonProperty("s")]
  public string Symbol { get; set; }

  [JsonProperty("d")]
  public List<List<string>> DataList { get; set; }
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
}
*/
/*
{
{
  "topic": "trades-100.BTCUSDT",
  "type": "delta",
  "data": {
    "s": "BTCUSDT",
    "d": [
      [
        "0-",
        "26574.50",
        "0.25",
        "2023-03-17T13:32:02Z",
        "a",
        "9ca02a95-7b16-5967-a333-57e9aa654746",
        "0"
      ],
      [
        "-",
        "26573.00",
        "0.75",
        "2023-03-17T13:32:02Z",
        "a",
        "25a1e946-0e2c-5e5d-b486-c6247ff50cad",
        "0"
      ]
    ]
  },
  "cs": 16250033,
  "ts": 1679059922985128
}

}
*/

