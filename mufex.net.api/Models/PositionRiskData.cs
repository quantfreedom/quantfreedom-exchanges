using Newtonsoft.Json;

namespace mufex.net.api.Models;

public class PositionRiskData
{
    public string? category { get; set; }

    [JsonProperty("list")]
    public List<PositionRiskDataList>? list { get; set; }
}

public class PositionRiskDataList
{
    public int? id { get; set; }
    public string? symbol { get; set; }
    public int? limit { get; set; }
    public double? maintainMargin { get; set; }
    public double? initialMargin { get; set; }

    [JsonProperty("section")]
    public List<int>? section { get; set; }
    public int? isLowestRisk { get; set; }
    public double? maxLeverage { get; set; }
}