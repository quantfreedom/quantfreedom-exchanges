using Newtonsoft.Json;

namespace Models.Mufex;
public class MufexOpenOrdersDataList
{
  [JsonProperty("list")]
  public List<MufexOpenOrdersData>? list { get; set; }
  public string? nextPageCursor { get; set; }
}

public class MufexOpenOrdersData
{
  public string? symbol { get; set; }
  public string? orderId { get; set; }
  public string? side { get; set; }
  public string? orderType { get; set; }
  public string? stopOrderType { get; set; }
  public double? price { get; set; }
  public double? qty { get; set; }
  public string? timeInForce { get; set; }
  public string? orderStatus { get; set; }
  public double? triggerPrice { get; set; }
  public string? orderLinkId { get; set; }
  public long? createdTime { get; set; }
  public long? updatedTime { get; set; }
  public double? takeProfit { get; set; }
  public double? stopLoss { get; set; }
  public string? tpTriggerBy { get; set; }
  public string? slTriggerBy { get; set; }
  public string? triggerBy { get; set; }
  public bool? reduceOnly { get; set; }
  public double? leavesQty { get; set; }
  public double? leavesValue { get; set; }
  public double? cumExecQty { get; set; }
  public double? cumExecValue { get; set; }
  public double? cumExecFee { get; set; }
  public int? triggerDirection { get; set; }

}

/*
https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-contract_getopenorder
{
  "code": 0,
  "message": "OK",
  "data":  {
    "list": [
      {
        "symbol": "XRPUSDT",
        "orderId": "db8b74b3-72d3-4264-bf3f-52d39b41956e",
        "side": "Sell",
        "orderType": "Limit",
        "stopOrderType": "Stop",
        "price": "0.4000",
        "qty": "15",
        "timeInForce": "GoodTillCancel",
        "orderStatus": "UnTriggered",
        "triggerPrice": "0.1000",
        "orderLinkId": "x002",
        "createdTime": "1658901865082",
        "updatedTime": "1658902610748",
        "takeProfit": "0.2000",
        "stopLoss": "1.6000",
        "tpTriggerBy": "UNKNOWN",
        "slTriggerBy": "UNKNOWN",
        "triggerBy": "MarkPrice",
        "reduceOnly": false,
        "leavesQty": "15",
        "leavesValue": "6",
        "cumExecQty": "0",
        "cumExecValue": "0",
        "cumExecFee": "0",
        "triggerDirection": 2
      }
    ],
    "nextPageCursor": ""
  },
  "ext_info": {},
  "time": 1658902847238
}
*/