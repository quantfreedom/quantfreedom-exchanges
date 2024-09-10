using Newtonsoft.Json;

namespace Models.Mufex;

public class MufexAllSymbolInfoDataList
{
    public string? category { get; set; }

    [JsonProperty("list")]
    public List<MufexAllSymbolInfoData>? list { get; set; }
}

public class MufexAllSymbolInfoData
{
    public string? symbol { get; set; }
    public string? contractType { get; set; }
    public string? status { get; set; }
    public string? baseCoin { get; set; }
    public string? quoteCoin { get; set; }
    public long? launchTime { get; set; }
    public long? deliveryTime { get; set; }
    public double? deliveryFeeRate { get; set; }
    public int? priceScale { get; set; }

    [JsonProperty("leverageFilter")]
    public MufexLeverageFilter? leverageFilter { get; set; }

    [JsonProperty("priceFilter")]
    public MufexPriceFilter? priceFilter { get; set; }

    [JsonProperty("lotSizeFilter")]
    public MufexLotSizeFilter? lotSizeFilter { get; set; }
    public bool? unifiedMarginTrade { get; set; }
    public int? fundingInterval { get; set; }
    public string? settleCoin { get; set; }
}

public class MufexLeverageFilter
{
    public double? minLeverage { get; set; }
    public double? maxLeverage { get; set; }
    public double? leverageStep { get; set; }
}

public class MufexPriceFilter
{
    public double? minPrice { get; set; }
    public double? maxPrice { get; set; }
    public double? tickSize { get; set; }
}

public class MufexLotSizeFilter
{
    public double? maxTradingQty { get; set; }
    public double? minTradingQty { get; set; }
    public double? qtyStep { get; set; }
    public double? postOnlyMaxOrderQty { get; set; }
}

/*
{
    "code": 0,
    "message": "OK",
    "data":  {
        "category": "linear",
        "list": [
            {
                "symbol": "BTCUSDT",
                "contractType": "LinearPerpetual",
                "status": "Trading",
                "baseCoin": "BTC",
                "quoteCoin": "USDT",
                "launchTime": "1584230400000",
                "deliveryTime": "0",
                "deliveryFeeRate": "",
                "priceScale": "2",
                "leverageFilter": {
                    "minLeverage": "1",
                    "maxLeverage": "100.00",
                    "leverageStep": "0.01"
                },
                "priceFilter": {
                    "minPrice": "0.50",
                    "maxPrice": "999999.00",
                    "tickSize": "0.50"
                },
                "lotSizeFilter": {
                    "maxTradingQty": "100.000",
                    "minTradingQty": "0.001",
                    "qtyStep": "0.001",
                    "postOnlyMaxOrderQty": "1000.000"
                },
                "unifiedMarginTrade": true,
                "fundingInterval": 480,
                "settleCoin": "USDT"
            }
        ],
        "nextPageCursor": ""
    },
    "ext_info": {},
    "time": 1670838442302
}
*/