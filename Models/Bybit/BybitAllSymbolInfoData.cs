using Newtonsoft.Json;

namespace Models.Bybit;

public class BybitAllSymbolInfoDataList
{
    public string? category { get; set; }

    [JsonProperty("list")]
    public List<BybitAllSymbolInfoData>? list { get; set; }
    public string? nextPageCursor { get; set; }
}

public class BybitAllSymbolInfoData
{
    public string? symbol { get; set; }
    public string? contractType { get; set; }
    public string? status { get; set; }
    public string? baseCoin { get; set; }
    public string? quoteCoin { get; set; }
    public long? launchTime { get; set; }
    public long? deliveryTime { get; set; }
    public decimal? deliveryFeeRate { get; set; }
    public int? priceScale { get; set; }

    [JsonProperty("leverageFilter")]
    public BybitLeverageFilter? leverageFilter { get; set; }

    [JsonProperty("priceFilter")]
    public BybitPriceFilter? priceFilter { get; set; }

    [JsonProperty("lotSizeFilter")]
    public BybitLotSizeFilter? lotSizeFilter { get; set; }
    public bool? unifiedMarginTrade { get; set; }
    public int? fundingInterval { get; set; }
    public string? settleCoin { get; set; }
    public string? copyTrading { get; set; }
    public decimal? upperFundingRate { get; set; }
    public decimal? lowerFundingRate { get; set; }
}

public class BybitLeverageFilter
{
    public decimal? minLeverage { get; set; }
    public decimal? maxLeverage { get; set; }
    public decimal? leverageStep { get; set; }
}

public class BybitPriceFilter
{
    public decimal? minPrice { get; set; }
    public decimal? maxPrice { get; set; }
    public decimal? tickSize { get; set; }
}

public class BybitLotSizeFilter
{
    public decimal? maxTradingQty { get; set; }
    public decimal? minTradingQty { get; set; }
    public decimal? qtyStep { get; set; }
    public decimal? postOnlyMaxOrderQty { get; set; }
    public decimal? minNotionalValue { get; set; }
}

/*
https://bybit-exchange.github.io/docs/v5/market/instrument

// official USDT Perpetual instrument structure

{
    "retCode": 0,
    "retMsg": "OK",
    "result": {
        "category": "linear",
        "list": [
            {
                "symbol": "BTCUSDT",
                "contractType": "LinearPerpetual",
                "status": "Trading",
                "baseCoin": "BTC",
                "quoteCoin": "USDT",
                "launchTime": "1585526400000",
                "deliveryTime": "0",
                "deliveryFeeRate": "",
                "priceScale": "2",
                "leverageFilter": {
                    "minLeverage": "1",
                    "maxLeverage": "100.00",
                    "leverageStep": "0.01"
                },
                "priceFilter": {
                    "minPrice": "0.10",
                    "maxPrice": "199999.80",
                    "tickSize": "0.10"
                },
                "lotSizeFilter": {
                    "maxOrderQty": "100.000",
                    "maxMktOrderQty": "100.000",
                    "minOrderQty": "0.001",
                    "qtyStep": "0.001",
                    "postOnlyMaxOrderQty": "1000.000",
                    "minNotionalValue": "5"
                },
                "unifiedMarginTrade": true,
                "fundingInterval": 480,
                "settleCoin": "USDT",
                "copyTrading": "both",
                "upperFundingRate": "0.00375",
                "lowerFundingRate": "-0.00375"
            }
        ],
        "nextPageCursor": ""
    },
    "retExtInfo": {},
    "time": 1707186451514
}

*/