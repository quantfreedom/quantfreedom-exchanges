﻿using Newtonsoft.Json;

namespace Models.Bybit;
public class BybitGeneralResponse<T>
{
    // do I need to have this json property? TODO: check

    [JsonProperty("retCode")]
    public int code { get; set; }

    [JsonProperty("retMsg")]
    public string message { get; set; }

    [JsonProperty("result")]
    public T? data { get; set; }

    [JsonProperty("retExtInfo")]
    public Dictionary<string, object>? ext_info { get; set; }

    [JsonProperty("time")]
    public long? time { get; set; }
}

/*
{
    "retCode": 0,
    "retMsg": "OK",
    "result": {
        "list": [
            {
                "totalEquity": "3.31216591",
                "accountIMRate": "0",
                "totalMarginBalance": "3.00326056",
                "totalInitialMargin": "0",
                "accountType": "UNIFIED",
                "totalAvailableBalance": "3.00326056",
                "accountMMRate": "0",
                "totalPerpUPL": "0",
                "totalWalletBalance": "3.00326056",
                "accountLTV": "0",
                "totalMaintenanceMargin": "0",
                "coin": [
                    {
                        "availableToBorrow": "3",
                        "bonus": "0",
                        "accruedInterest": "0",
                        "availableToWithdraw": "0",
                        "totalOrderIM": "0",
                        "equity": "0",
                        "totalPositionMM": "0",
                        "usdValue": "0",
                        "spotHedgingQty": "0.01592413",
                        "unrealisedPnl": "0",
                        "collateralSwitch": true,
                        "borrowAmount": "0.0",
                        "totalPositionIM": "0",
                        "walletBalance": "0",
                        "cumRealisedPnl": "0",
                        "locked": "0",
                        "marginCollateral": true,
                        "coin": "BTC"
                    }
                ]
            }
        ]
    },
    "retExtInfo": {},
    "time": 1690872862481
}
*/