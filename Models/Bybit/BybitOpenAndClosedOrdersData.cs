using Newtonsoft.Json;

namespace Models.Bybit;
public class BybitOpenAndClosedOrdersDataList
{
  [JsonProperty("list")]
  public List<BybitOpenAndClosedOrdersData>? list { get; set; }
  
  [JsonProperty("nextPageCursor")]
  public string? nextPageCursor { get; set; }

  [JsonProperty("category")]
  public string? category { get; set; }
}

public class BybitOpenAndClosedOrdersData
{
  public string? orderId { get; set; }
  public string? orderLinkId { get; set; }
  public string? blockTradeId { get; set; }
  public string? symbol { get; set; }
  public decimal? price { get; set; }
  public decimal? qty { get; set; }
  public string? side { get; set; }
  public string? isLeverage { get; set; }
  public int? positionIdx { get; set; }
  public string? orderStatus { get; set; }
  public string? cancelType { get; set; }
  public string? rejectReason { get; set; }
  public decimal? avgPrice { get; set; }
  public decimal? leavesQty { get; set; }
  public decimal? leavesValue { get; set; }
  public decimal? cumExecQty { get; set; }
  public decimal? cumExecValue { get; set; }
  public decimal? cumExecFee { get; set; }
  public string? timeInForce { get; set; }
  public string? orderType { get; set; }
  public string? stopOrderType { get; set; }
  public string? orderIv { get; set; }
  public decimal? triggerPrice { get; set; }
  public decimal? takeProfit { get; set; }
  public decimal? stopLoss { get; set; }
  public string? tpTriggerBy { get; set; }
  public string? slTriggerBy { get; set; }
  public int? triggerDirection { get; set; }
  public string? triggerBy { get; set; }
  public string? lastPriceOnCreated { get; set; }
  public bool? reduceOnly { get; set; }
  public bool? closeOnTrigger { get; set; }
  public string? smpType { get; set; }
  public int? smpGroup { get; set; }
  public string? smpOrderId { get; set; }
  public string? tpslMode { get; set; }
  public string? tpLimitPrice { get; set; }
  public string? slLimitPrice { get; set; }
  public string? placeType { get; set; }
  public long? createdTime { get; set; }
  public long? updatedTime { get; set; }
}

/*
https://bybit-exchange.github.io/docs/v5/order/open-order
{
    "retCode": 0,
    "retMsg": "OK",
    "result": {
        "list": [
            {
                "orderId": "fd4300ae-7847-404e-b947-b46980a4d140",
                "orderLinkId": "test-000005",
                "blockTradeId": "",
                "symbol": "ETHUSDT",
                "price": "1600.00",
                "qty": "0.10",
                "side": "Buy",
                "isLeverage": "",
                "positionIdx": 1,
                "orderStatus": "New",
                "cancelType": "UNKNOWN",
                "rejectReason": "EC_NoError",
                "avgPrice": "0",
                "leavesQty": "0.10",
                "leavesValue": "160",
                "cumExecQty": "0.00",
                "cumExecValue": "0",
                "cumExecFee": "0",
                "timeInForce": "GTC",
                "orderType": "Limit",
                "stopOrderType": "UNKNOWN",
                "orderIv": "",
                "triggerPrice": "0.00",
                "takeProfit": "2500.00",
                "stopLoss": "1500.00",
                "tpTriggerBy": "LastPrice",
                "slTriggerBy": "LastPrice",
                "triggerDirection": 0,
                "triggerBy": "UNKNOWN",
                "lastPriceOnCreated": "",
                "reduceOnly": false,
                "closeOnTrigger": false,
                "smpType": "None",
                "smpGroup": 0,
                "smpOrderId": "",
                "tpslMode": "Full",
                "tpLimitPrice": "",
                "slLimitPrice": "",
                "placeType": "",
                "createdTime": "1684738540559",
                "updatedTime": "1684738540561"
            }
        ],
        "nextPageCursor": "page_args%3Dfd4300ae-7847-404e-b947-b46980a4d140%26symbol%3D6%26",
        "category": "linear"
    },
    "retExtInfo": {},
    "time": 1684765770483
}
*/