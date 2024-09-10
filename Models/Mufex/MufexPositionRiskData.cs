using Newtonsoft.Json;

namespace Models.Mufex;

public class MufexPositionRiskDataList
{
    public string? category { get; set; }

    [JsonProperty("list")]
    public List<MufexPositionRiskData>? list { get; set; }
}

public class MufexPositionRiskData
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