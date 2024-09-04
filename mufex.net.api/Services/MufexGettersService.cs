using Newtonsoft.Json;
using mufex.net.api.Models;
using Newtonsoft.Json.Linq;

namespace mufex.net.api.Services;

public class MufexGettersService
{
    private readonly MufexApiService apiService;
    public MufexGettersService(
        MufexApiService apiService
        )
    {
        this.apiService = apiService;
    }

    // Private Endpoints

    /// <summary>
    /// Obtain wallet balance, query asset information of each currency, and account risk rate information. By default, currency information with assets or liabilities of 0 is not returned.
    /// The trading of UTA inverse contracts is conducted through the CONTRACT wallet.
    /// To get Funding wallet balance, please go to this endpoint
    /// https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-balance
    /// </summary>
    /// <param name="coin"></param>
    /// <returns> Account Balance </returns>
    public async Task<string?> GetAccountBalance(string? coin = null)
    {
        var query = new Dictionary<string, object>();

        MufexParametersUtils.AddOptionalParameters(
            query,
            ("coin", coin)
        );

        var result = await this.apiService.SendSignedAsync<string>(
            requestUri: "/private/v1/account/balance",
            httpMethod: HttpMethod.Get,
            query: query
            );
        var jsonResult = JsonConvert.DeserializeObject<GeneralResponse<GetWalletBalanceData>>(result);
        var jsonResultList = jsonResult.data.list;
        string jsonFormatted = JValue.Parse(result).ToString(Formatting.Indented);
        Console.WriteLine(jsonFormatted);
        return result;
    }
    /// <summary>
    /// https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-tradingfeerate
    /// </summary>
    /// <param name="coin"></param>
    /// <returns> Account Balance </returns>
    public async Task<string?> GetTradingFeeRates(string? symbol = null)
    {
        var query = new Dictionary<string, object>();

        MufexParametersUtils.AddOptionalParameters(
            query,
            ("symbol", symbol)
        );

        var result = await this.apiService.SendSignedAsync<string>(
            requestUri: "/private/v1/account/trade-fee",
            httpMethod: HttpMethod.Get,
            query: query
            );
        var jsonResult = JsonConvert.DeserializeObject<GeneralResponse<TradingFeeData>>(result);
        var jsonResultList = jsonResult.data.list;
        string jsonFormatted = JValue.Parse(result).ToString(Formatting.Indented);
        Console.WriteLine(jsonFormatted);
        return result;
    }

    // Public Endpoints
    /// <summary>
    /// Get Risk Limit
    /// https://www.mufex.finance/apidocs/derivatives/contract/index.html?console#t-dv_risklimithead
    /// </summary>
    /// <param name="coin"></param>
    /// <returns> Account Balance </returns>
    public async Task<string?> GetPositionRisk(
        string symbol,
        string category = "linear"
    )
    {
        var query = new Dictionary<string, object>{
            { "symbol", symbol },
            { "category", category }
        };

        var result = await this.apiService.SendSignedAsync<string>(
            requestUri: "/public/v1/position-risk",
            httpMethod: HttpMethod.Get,
            query: query
            );
        var jsonResult = JsonConvert.DeserializeObject<GeneralResponse<PositionRiskData>>(result);
        var jsonResultList = jsonResult.data.list;
        string jsonFormatted = JValue.Parse(result).ToString(Formatting.Indented);
        Console.WriteLine(jsonFormatted);
        return result;
    }
    /// <summary>
    /// https://www.mufex.finance/apidocs/derivatives/contract/index.html?console#t-dv_myposition
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="settleCoin"></param>
    /// <param name="limit"></param>
    /// <returns></returns>

    public async Task<List<PositionInfoData>?> GetPositionInfo(
        string? symbol = null,
        string? settleCoin = null,
        int? limit = null
    )
    {
        var query = new Dictionary<string, object>();

        MufexParametersUtils.AddOptionalParameters(
            query,
            ("symbol", symbol),
            ("settleCoin", settleCoin),
            ("limit", limit)
        );

        var result = await this.apiService.SendSignedAsync<string>(
            requestUri: "/private/v1/account/positions",
            httpMethod: HttpMethod.Get,
            query: query
            );
        var jsonResult = JsonConvert.DeserializeObject<GeneralResponse<PositionInfoDataList>>(result);
        var jsonResultList = jsonResult.data.list;
        return jsonResultList;
    }
    /// <summary>
    /// https://www.mufex.finance/apidocs/derivatives/contract/index.html?console#t-usertraderecords
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="orderId"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="execType"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public async Task<List<FilledOrdersData>?> GetFilledOrders(
        string symbol,
        string? orderId = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        ExecutionType? execType = null,
        int? limit = null
    )
    {
        long? startTimeUtcTimestamp = MufexParametersUtils.GetUnixTimeStampLong(startTime);
        long? endTimeUtcTimestamp = MufexParametersUtils.GetUnixTimeStampLong(endTime);

        var query = new Dictionary<string, object>{
            { "symbol", symbol }
        };

        MufexParametersUtils.AddOptionalParameters(
            query,
            ("orderId", orderId),
            ("startTime", startTimeUtcTimestamp),
            ("endTime", endTimeUtcTimestamp),
            ("execType", (execType == null) ? null : execType.Value),
            ("limit", limit)
        );

        var result = await this.apiService.SendSignedAsync<string>(
            requestUri: "/private/v1/trade/fills",
            httpMethod: HttpMethod.Get,
            query: query
            );
        var jsonResult = JsonConvert.DeserializeObject<GeneralResponse<FilledOrdersDataList>>(result);
        var jsonResultList = jsonResult.data.list;
        return jsonResultList;
    }

    /// <summary>
    /// https://www.mufex.finance/apidocs/derivatives/contract/index.html?console#t-dv_closedprofitandloss
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public async Task<string?> GetPnL(
        string symbol,
        DateTime? startTime = null,
        DateTime? endTime = null,
        int? limit = null
    )
    {
        long? startTimeUtcTimestamp = MufexParametersUtils.GetUnixTimeStampLong(startTime);
        long? endTimeUtcTimestamp = MufexParametersUtils.GetUnixTimeStampLong(endTime);

        var query = new Dictionary<string, object>{
            { "symbol", symbol }
        };

        MufexParametersUtils.AddOptionalParameters(
            query,
            ("startTime", startTimeUtcTimestamp),
            ("endTime", endTimeUtcTimestamp),
            ("limit", limit)
        );

        var result = await this.apiService.SendSignedAsync<string>(
            requestUri: "/private/v1/account/closed-pnl",
            httpMethod: HttpMethod.Get,
            query: query
            );
        var jsonResult = JsonConvert.DeserializeObject<GeneralResponse<GetPnLData>>(result);
        var jsonResultList = jsonResult.data.list;
        string jsonFormatted = JValue.Parse(result).ToString(Formatting.Indented);
        Console.WriteLine(jsonFormatted);
        return result;
    }
    public async Task<double?> GetLatestPnL(
        string symbol
    )
    {
        var result = await this.GetPnL(
            symbol: symbol,
            limit: 1
        );
        var jsonResult = JsonConvert.DeserializeObject<GeneralResponse<GetPnLData>>(result);
        var jsonResultList = jsonResult.data.list;
        double latestPnL = jsonResultList[0].closedPnl.Value;
        return latestPnL;
    }

    /// <summary>
    /// https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-dv_walletrecords
    /// </summary>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="coin"></param>
    /// <param name="walletFundType"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public async Task<string?> GetWalletFundingRecords(
        DateTime? startTime = null,
        DateTime? endTime = null,
        string? coin = null,
        WalletFundType? walletFundType = null,
        int? limit = null
    )
    {
        long? startTimeUtcTimestamp = MufexParametersUtils.GetUnixTimeStampLong(startTime);
        long? endTimeUtcTimestamp = MufexParametersUtils.GetUnixTimeStampLong(endTime);

        var query = new Dictionary<string, object>();

        MufexParametersUtils.AddOptionalParameters(
            query,
            ("startTime", startTimeUtcTimestamp),
            ("endTime", endTimeUtcTimestamp),
            ("coin", coin),
            ("walletFundType", (walletFundType == null) ? null : walletFundType.Value),
            ("limit", limit)
        );

        var result = await this.apiService.SendSignedAsync<string>(
            requestUri: "/private/v1/account/bills",
            httpMethod: HttpMethod.Get,
            query: query
            );
        var jsonResult = JsonConvert.DeserializeObject<GeneralResponse<GetPnLData>>(result);
        var jsonResultList = jsonResult.data.list;
        string jsonFormatted = JValue.Parse(result).ToString(Formatting.Indented);
        Console.WriteLine(jsonFormatted);
        return result;
    }
    /// <summary>
    /// https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-dv_instrhead
    /// </summary>
    /// <param name="category"></param>
    /// <param name="symbol"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public async Task<string?> GetAllSymbolsInfo(
        string category = "linear",
        string? symbol = null,
        int? limit = null
    )
    {
        var query = new Dictionary<string, object>{
            { "category", category }
        };

        MufexParametersUtils.AddOptionalParameters(
            query,
            ("symbol", symbol),
            ("limit", limit)
        );

        var result = await this.apiService.SendSignedAsync<string>(
            requestUri: "/public/v1/instruments",
            httpMethod: HttpMethod.Get,
            query: query
            );
        var jsonResult = JsonConvert.DeserializeObject<GeneralResponse<AllSymbolInfoData>>(result);
        var jsonResultList = jsonResult.data.list;
        string jsonFormatted = JValue.Parse(result).ToString(Formatting.Indented);
        Console.WriteLine(jsonFormatted);
        return result;
    }

    public async Task<List<string>> GetListOfSymbols()
    {
        var symbols = new List<string>();

        var result = await this.GetAllSymbolsInfo();
        var jsonResult = JsonConvert.DeserializeObject<GeneralResponse<AllSymbolInfoData>>(result);
        var jsonResultList = jsonResult.data.list;
        foreach (var symbol in jsonResultList)
        {
            symbols.Add(symbol.symbol);
        }
        symbols.Sort();
        return symbols;
    }
    /// <summary>
    /// https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-contract_getorder
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="orderId"></param>
    /// <param name="orderLinkID"></param>
    /// <param name="orderStatus"></param>
    /// <param name="orderFiler"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public async Task<GeneralResponse<OrderHistoryData>?> GetOrderHistory(
        string symbol,
        string? orderId = null,
        string? orderLinkID = null,
        OrderStatusType? orderStatus = null,
        OrderFilterType? orderFiler = null,
        int? limit = null
    )
    {
        var query = new Dictionary<string, object>{
            { "symbol", symbol }
        };

        MufexParametersUtils.AddOptionalParameters(
            query,
            ("orderId", orderId),
            ("orderLinkID", orderLinkID),
            ("orderStatus", (orderStatus == null) ? null : orderStatus.Value),
            ("orderFiler", (orderFiler == null) ? null : orderFiler.Value),
            ("limit", limit)
        );

        var result = await this.apiService.SendSignedAsync<string>(
            requestUri: "/private/v1/trade/orders",
            httpMethod: HttpMethod.Get,
            query: query
            );
        var jsonResult = JsonConvert.DeserializeObject<GeneralResponse<OrderHistoryData>>(result);
        return jsonResult;
    }
    /// <summary>
    /// https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-contract_getopenorder
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="orderId"></param>
    /// <param name="orderLinkID"></param>
    /// <param name="orderStatus"></param>
    /// <param name="orderFiler"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public async Task<List<OpenOrdersData>?> GetOpenOrders(
        string? symbol = null,
        string? orderId = null,
        string? orderLinkID = null,
        OrderStatusType? orderStatus = null,
        OrderFilterType? orderFiler = null,
        int? limit = null
    )
    {
        var query = new Dictionary<string, object>();

        MufexParametersUtils.AddOptionalParameters(
            query,
            ("symbol", symbol),
            ("orderId", orderId),
            ("orderLinkID", orderLinkID),
            ("orderStatus", (orderStatus == null) ? null : orderStatus.Value),
            ("orderFiler", (orderFiler == null) ? null : orderFiler.Value),
            ("limit", limit)
        );

        var result = await this.apiService.SendSignedAsync<string>(
            requestUri: "/private/v1/trade/activity-orders",
            httpMethod: HttpMethod.Get,
            query: query
            );
        var jsonResult = JsonConvert.DeserializeObject<GeneralResponse<OpenOrdersDataList>>(result);
        var jsonResultList = jsonResult.data.list;
        return jsonResultList;
    }

    public async Task<(double, double)> GetFeePercents(
        string symbol
    )
    {
        var result = await GetTradingFeeRates(symbol: symbol);
        var jsonResult = JsonConvert.DeserializeObject<GeneralResponse<TradingFeeData>>(result);
        var jsonResultList = jsonResult.data.list;
        double marketFeePct = jsonResultList[0].takerFeeRate.Value;
        double limitFeePct = jsonResultList[0].makerFeeRate.Value;

        return (marketFeePct, limitFeePct);
    }
    public async Task<double> GetMmrPercent(
        string symbol
    )
    {
        var result = await GetPositionRisk(symbol: symbol);
        var jsonResult = JsonConvert.DeserializeObject<GeneralResponse<PositionRiskData>>(result);
        var jsonResultList = jsonResult.data.list;
        double mmrPct = jsonResultList[0].maintainMargin.Value;

        return mmrPct;
    }

    public async Task<(double, double, double, double, double, double, double)> GetMinMaxLeverageAndAssetSize(
        string symbol
    )
    {
        var result = await GetAllSymbolsInfo(symbol: symbol);
        var jsonResult = JsonConvert.DeserializeObject<GeneralResponse<AllSymbolInfoData>>(result);
        var jsonResultList = jsonResult.data.list[0];
        double minLev = jsonResultList.leverageFilter.minLeverage.Value;
        double maxLev = jsonResultList.leverageFilter.maxLeverage.Value;
        double levStep = jsonResultList.leverageFilter.leverageStep.Value;
        double minAssetSize = jsonResultList.lotSizeFilter.minTradingQty.Value;
        double maxAssetSize = jsonResultList.lotSizeFilter.maxTradingQty.Value;
        double assetSizeStep = jsonResultList.lotSizeFilter.qtyStep.Value;
        double priceStep = jsonResultList.priceFilter.tickSize.Value;
        return (minLev, maxLev, levStep, minAssetSize, maxAssetSize, assetSizeStep, priceStep);
    }
}
