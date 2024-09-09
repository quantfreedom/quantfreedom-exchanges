using Newtonsoft.Json;

namespace mufex.net.api.Models;
public class CandleData
{
    [JsonProperty("category")]
    public string? Category { get; set; }

    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    [JsonProperty("interval")]
    public string? Interval { get; set; }

    [JsonProperty("list")]
    public List<List<string>> List { get; set; }
}

public class CandleDataParsed
{
    public long? Timestamp { get; set; }
    public double? Open { get; set; }
    public double? High { get; set; }
    public double? Low { get; set; }
    public double? Close { get; set; }
    public double? Volume { get; set; }
}
/*
{
    "code": 0,
    "message": "OK",,
    "data": {
      "category":"linear",
      "symbol":"BTCUSDT",
      "interval":"1",
      "list":[
      [
      "1621162800000",
      "49592.43",
      "49644.91",
      "49342.37",
      "49349.42",
      "1451.59",
      "2.4343353100000003"
      ]
    ]
    },
    "ext_info": {},
    "time": 1669802294719
}
*/