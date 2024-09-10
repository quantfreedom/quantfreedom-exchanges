using Newtonsoft.Json;

namespace Models.Bybit;

public class BybitWalletBalanceData
{
    [JsonProperty("list")]
    public List<BybitWalletBalanceDataList>? list { get; set; }
}

public class BybitWalletBalanceDataList
{
    public decimal totalEquity { get; set; }
    public decimal accountIMRate { get; set; }
    public decimal totalMarginBalance { get; set; }
    public decimal totalInitialMargin { get; set; }
    public string accountType { get; set; }
    public decimal totalAvailableBalance { get; set; }
    public decimal accountMMRate { get; set; }
    public decimal totalPerpUPL { get; set; }
    public decimal totalWalletBalance { get; set; }
    public decimal accountLTV { get; set; }
    public decimal totalMaintenanceMargin { get; set; }
    public List<BybitWalletBalanceDataCoinList>? coin { get; set; }
}

public class BybitWalletBalanceDataCoinList
{
    public decimal availableToBorrow { get; set; }
    public decimal bonus { get; set; }
    public decimal accruedInterest { get; set; }
    public decimal availableToWithdraw { get; set; }
    public decimal totalOrderIM { get; set; }
    public decimal equity { get; set; }
    public decimal totalPositionMM { get; set; }
    public decimal usdValue { get; set; }
    public decimal spotHedgingQty { get; set; }
    public decimal unrealisedPnl { get; set; }
    public bool collateralSwitch { get; set; }
    public decimal borrowAmount { get; set; }
    public decimal totalPositionIM { get; set; }
    public decimal walletBalance { get; set; }
    public decimal cumRealisedPnl { get; set; }
    public decimal locked { get; set; }
    public bool marginCollateral { get; set; }
    public string coin { get; set; }
}


/*
https://bybit-exchange.github.io/docs/v5/account/wallet-balance
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