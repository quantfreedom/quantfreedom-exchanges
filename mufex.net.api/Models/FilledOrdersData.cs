using Newtonsoft.Json;

namespace mufex.net.api.Models;

public class FilledOrdersDataList
{

    [JsonProperty("list")]
    public List<FilledOrdersData>? list { get; set; }
}

public class FilledOrdersData
{
    public string? symbol { get; set; }
    public double? execFee { get; set; }
    public string? execId { get; set; }
    public double? execPrice { get; set; }
    public double? execQty { get; set; }
    public string? execType { get; set; }
    public double? execValue { get; set; }
    public double? feeRate { get; set; }
    public string? lastLiquidityInd { get; set; }
    public double? leavesQty { get; set; }
    public string? orderId { get; set; }
    public string? orderLinkId { get; set; }
    public double? orderPrice { get; set; }
    public double? orderQty { get; set; }
    public string? orderType { get; set; }
    public string? stopOrderType { get; set; }
    public string? side { get; set; }
    public long? execTime { get; set; }
    public double? closedSize { get; set; }
    public long? crossSeq { get; set; }
}


/*
https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-usertraderecords
{
    "code": 0,
        "message": "OK",
        "data":  {
        "list": [
            {
                "symbol": "BTCUSDT",
                "execFee": "0.00293025",
                "execId": "16bff97b-0e04-4303-82b5-1b1134f15695",
                "execPrice": "0.00",
                "execQty": "0.001",
                "execType": "Funding",
                "execValue": "29.30248",
                "feeRate": "0.0001",
                "lastLiquidityInd": "UNKNOWN",
                "leavesQty": "0.000",
                "orderId": "1690588800-BTCUSDT-139688-Buy",
                "orderLinkId": "",
                "orderPrice": "0.00",
                "orderQty": "0.000",
                "orderType": "UNKNOWN",
                "stopOrderType": "UNKNOWN",
                "side": "Buy",
                "execTime": "1690588800000",
                "closedSize": "0.000",
                "crossSeq": "41019846"
            },
            {
                "symbol": "BTCUSDT",
                "execFee": "0.00293787",
                "execId": "d9439111-d9ee-4cf3-90e1-85e8f148b6a3",
                "execPrice": "0.00",
                "execQty": "0.001",
                "execType": "Funding",
                "execValue": "29.3787",
                "feeRate": "0.0001",
                "lastLiquidityInd": "UNKNOWN",
                "leavesQty": "0.000",
                "orderId": "1690560000-BTCUSDT-139688-Buy",
                "orderLinkId": "",
                "orderPrice": "0.00",
                "orderQty": "0.000",
                "orderType": "UNKNOWN",
                "stopOrderType": "UNKNOWN",
                "side": "Buy",
                "execTime": "1690560000000",
                "closedSize": "0.000",
                "crossSeq": "40849025"
            }
        ],
            "nextPageCursor": ""
    },
    "ext_info": {},
    "time": 1658911518442
}
*/