using Newtonsoft.Json;

namespace mufex.net.api.Models;

public class OrderHistoryData
{
    [JsonProperty("list")]
    public List<OrderHistoryDataList>? list { get; set; }
    public string? nextPageCursor { get; set; }
}

public class OrderHistoryDataList
{
    public string? symbol { get; set; }
    public string? side { get; set; }
    public string? orderType { get; set; }
    public double? price { get; set; }
    public double? qty { get; set; }
    public bool? reduceOnly { get; set; }
    public string? timeInForce { get; set; }
    public string? orderStatus { get; set; }
    public double? leavesQty { get; set; }
    public double? leavesValue { get; set; }
    public double? cumExecQty { get; set; }
    public double? cumExecValue { get; set; }
    public double? cumExecFee { get; set; }
    public double? lastPriceOnCreated { get; set; }
    public string? rejectReason { get; set; }
    public string? orderLinkId { get; set; }
    public long? createdTime { get; set; }
    public long? updatedTime { get; set; }
    public string? orderId { get; set; }
    public string? stopOrderType { get; set; }
    public double? takeProfit { get; set; }
    public double? stopLoss { get; set; }
    public string? tpTriggerBy { get; set; }
    public string? slTriggerBy { get; set; }
    public double? triggerPrice { get; set; }
    public bool? closeOnTrigger { get; set; }
    public int? triggerDirection { get; set; }
    public int? positionIdx { get; set; }

}

/*
{
    "code": 0,
    "message": "OK",
    "data":  {
        "list": [
            {
                "symbol": "BTCUSDT",
                "side": "Buy",
                "orderType": "Market",
                "price": "0.3431",
                "qty": "65",
                "reduceOnly": true,
                "timeInForce": "ImmediateOrCancel",
                "orderStatus": "Filled",
                "leavesQty": "0",
                "leavesValue": "0",
                "cumExecQty": "65",
                "cumExecValue": "21.3265",
                "cumExecFee": "0.0127959",
                "lastPriceOnCreated": "0.0000",
                "rejectReason": "EC_NoError",
                "orderLinkId": "",
                "createdTime": "1657526321499",
                "updatedTime": "1657526321504",
                "orderId": "ac0a8134-acb3-4ee1-a2d4-41891c9c46d7",
                "stopOrderType": "UNKNOWN",
                "takeProfit": "0.0000",
                "stopLoss": "0.0000",
                "tpTriggerBy": "UNKNOWN",
                "slTriggerBy": "UNKNOWN",
                "triggerPrice": "0.0000",
                "closeOnTrigger": true,
                "triggerDirection": 0,
                "positionIdx": 2
        ],
        "nextPageCursor": "K0crQkZRL0MyQVpiN0tVSDFTS0RlMk9DemNCWHZaRHp3aFZ4Y1Yza2MyWT0="
    },
    "ext_info": {},
    "time": 1658899014975
}
*/