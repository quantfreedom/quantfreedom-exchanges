using Newtonsoft.Json;

namespace mufex.net.api.Models;

public class WsTradeData
{

    [JsonProperty("topic")]
    public string? topic { get; set; }

    [JsonProperty("type")]
    public string? type { get; set; }

    [JsonProperty("data")]
    public WsGeneralResponseData data { get; set; }

    [JsonProperty("cs")]
    public long? crossSequence { get; set; }

    [JsonProperty("ts")]
    public long? timestamp { get; set; }
}

public class WsGeneralResponseData
{
    [JsonProperty("s")]
    public string? symbol { get; set; }

    [JsonProperty("d")]
    public List<List<string>> tradeList { get; set; }

}

public class WsTradeDataParsed
{
    public DateTime? Timestamp { get; set; }
    public double? AssetQty { get; set; }
    public double? UsdtQty { get; set; }
    public double? Price { get; set; }
    public string? IsBuyer { get; set; }
}