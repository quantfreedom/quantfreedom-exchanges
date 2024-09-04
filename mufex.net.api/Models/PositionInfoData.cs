using Newtonsoft.Json;

namespace mufex.net.api.Models;

public class PositionInfoDataList
{
    [JsonProperty("list")]
    public List<PositionInfoData>? list { get; set; }
}

public class PositionInfoData
{
    public int? positionIdx { get; set; }
    public int? riskId { get; set; }
    public string? symbol { get; set; }
    public string? side { get; set; }
    public double? size { get; set; }
    public double? positionValue { get; set; }
    public double? entryPrice { get; set; }
    public int? tradeMode { get; set; }
    public double? autoAddMargin { get; set; }
    public double? leverage { get; set; }
    public double? positionBalance { get; set; }
    public double? liqPrice { get; set; }
    public double? bustPrice { get; set; }
    public double? takeProfit { get; set; }
    public double? stopLoss { get; set; }
    public double? trailingStop { get; set; }
    public double? unrealisedPnl { get; set; }
    public long? createdTime { get; set; }
    public long? updatedTime { get; set; }
    public string? tpSlMode { get; set; }
    public int? riskLimitValue { get; set; }
    public double? activePrice { get; set; }
    public double? markPrice { get; set; }
    public double? cumRealisedPnl { get; set; }
    public double? positionMM { get; set; }
    public double? positionIM { get; set; }
    public string? positionStatus { get; set; }
    public double? sessionAvgPrice { get; set; }

}



/*

https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-balance

{
"code": 0,
    "message": "OK",
    "data":  {
    "list": [
        {
            "positionIdx": 0,
            "riskId": 11,
            "symbol": "ETHUSDT",
            "side": "None",
            "size": "0.00",
            "positionValue": "0",
            "entryPrice": "0",
            "tradeMode": 1,
            "autoAddMargin": 0,
            "leverage": "10",
            "positionBalance": "0",
            "liqPrice": "0.00",
            "bustPrice": "0.00",
            "takeProfit": "0.00",
            "stopLoss": "0.00",
            "trailingStop": "0.00",
            "unrealisedPnl": "0",
            "createdTime": "1659943372099",
            "updatedTime": "1669470547908",
            "tpSlMode": "Full",
            "riskLimitValue": "900000",
            "activePrice": "0.00",
            "markPrice": "1205.02",
            "cumRealisedPnl": "0.00",
            "positionMM": "",
            "positionIM": "",
            "positionStatus": "Normal",
            "sessionAvgPrice": "0.00",
        }
    ],
        "category": "linear",
        "nextPageCursor": ""
},
"ext_info": { },
"time": 1670836410404
*/