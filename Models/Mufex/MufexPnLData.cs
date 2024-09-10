using Newtonsoft.Json;

namespace Models.Mufex;
public class MufexPnLDataList
{

    [JsonProperty("list")]
    public List<MufexPnLData>? list { get; set; }
}

public class MufexPnLData
{

    public string? symbol { get; set; }
    public string? orderId { get; set; }
    public string? side { get; set; }
    public double? qty { get; set; }
    public double? orderPrice { get; set; }
    public string? orderType { get; set; }
    public string? execType { get; set; }
    public double? closedSize { get; set; }
    public double? cumEntryValue { get; set; }
    public double? avgEntryPrice { get; set; }
    public double? cumExitValue { get; set; }
    public double? avgExitPrice { get; set; }
    public double? closedPnl { get; set; }
    public int? fillCount { get; set; }
    public double? leverage { get; set; }
    public long? createdAt { get; set; }

}
/*
{
    "code": 0,
        "message": "OK",
        "data":  {
        "list": [
            {
                "symbol": "XRPUSDT",
                "orderId": "3834da81-2b9c-4f5b-a558-05091f26deda",
                "side": "Buy",
                "qty": "50",
                "orderPrice": "0.3541",
                "orderType": "Market",
                "execType": "Trade",
                "closedSize": "50",
                "cumEntryValue": "16.68",
                "avgEntryPrice": "0.3336",
                "cumExitValue": "16.865",
                "avgExitPrice": "0.3373",
                "closedPnl": "-0.2034435",
                "fillCount": "1",
                "leverage": "10",
                "createdAt": "1658914152212"
            }
        ],
            "nextPageCursor": "R21aYkRjQ0haRmcxeFJBanZNYm1Db01RWWdyV3YzZmdkUVNXUmtXdGpMMD0="
    },
    "ext_info": {},
    "time": 1658914264892
}
    */