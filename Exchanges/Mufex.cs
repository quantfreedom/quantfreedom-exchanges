using Exchanges.Exceptions;
using Exchanges.Services.Interfaces;
using Exchnages.Utils;
using Models.Mufex;
using Newtonsoft.Json;

namespace Exchanges.Mufex;

public class Mufex
{
    private readonly IApiService _apiService;
    private readonly string SignatureHeaderName = "MF-ACCESS-SIGN";
    public readonly string TestnetURL = "https://api.testnet.mufex.finance";
    public readonly string MainnetURL = "https://api.mufex.finance";
    public Mufex(
        IApiService apiService,
        string apiKey,
        string secretKey,
        bool useTestnet)
    {
        _apiService = apiService;
        _apiService.ApiKey = apiKey;
        _apiService.SecretKey = secretKey;
        _apiService.BaseUrl = useTestnet ? TestnetURL : MainnetURL;

    }
    public Mufex(IApiService apiService, bool useTestnet)
    {
        _apiService = apiService;
        _apiService.BaseUrl = useTestnet ? TestnetURL : MainnetURL;

    }

    private void GeneralResponseChecker<T>(MufexGeneralResponse<T> GeneralResponse)
    {
        var code = GeneralResponse.code;
        var message = GeneralResponse.message;
        if (code != 0 || message.ToLower() != "ok")
        {
            throw new ExchangeClientException(message: message, code: code);
        }
    }
    // https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-balance
    private Dictionary<string, string> BuildRequestHeaders(string currentTimeStamp, bool includeApiKey)
    {
        var headers = new Dictionary<string, string>
        {
            { "MF-ACCESS-SIGN-TYPE", "2" },
            { "MF-ACCESS-TIMESTAMP", currentTimeStamp },
            { "MF-ACCESS-RECV-WINDOW", "5000" },
            { "MF-ACCESS-API-KEY", _apiService.ApiKey },
            { "X-Referer", "AKF3CWKDT" }
        };

        return headers;
    }

    public async Task<List<MufexWalletBalanceData>?> GetAccountBalance(string? coin = null)
    {
        // https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-balance
        var endpoint = "/private/v1/account/balance";

        var query = new Dictionary<string, object>();

        ExchangeUtils.AddOptionalParameters(
            query,
            ("coin", coin)
        );

        var currentTimeStamp = ExchangeUtils.GetCurrentTimeStampString();
        var headers = BuildRequestHeaders(currentTimeStamp, true);

        var generalResponse = await _apiService.SendSignedGetAsync<MufexGeneralResponse<MufexWalletBalanceDataList>>(
            endPoint: endpoint,
            query: query,
            signatureHeaderName: this.SignatureHeaderName,
            currentTimeStamp: currentTimeStamp,
            headers: headers
            );
        GeneralResponseChecker(generalResponse);
        var responseList = generalResponse.data.list;
        return responseList;
    }

    public async Task<List<MufexOpenOrdersData>?> GetOpenOrders(
        string? symbol = null,
        string? orderId = null,
        string? orderLinkID = null,
        MufexOrderStatusType? orderStatus = null,
        MufexOrderFilterType? orderFiler = null,
        int? limit = null)
    {
        // https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-contract_getopenorder

        var endpoint = "/private/v1/trade/activity-orders";

        var query = new Dictionary<string, object>();

        ExchangeUtils.AddOptionalParameters(
            query,
            ("symbol", symbol),
            ("orderId", orderId),
            ("orderLinkID", orderLinkID),
            ("orderStatus", (orderStatus == null) ? null : orderStatus.Value),
            ("orderFiler", (orderFiler == null) ? null : orderFiler.Value),
            ("limit", limit)
        );

        var currentTimeStamp = ExchangeUtils.GetCurrentTimeStampString();
        var headers = BuildRequestHeaders(currentTimeStamp, true);

        var generalResponse = await _apiService.SendSignedGetAsync<MufexGeneralResponse<MufexOpenOrdersDataList>>(
            endPoint: endpoint,
            query: query,
            signatureHeaderName: this.SignatureHeaderName,
            currentTimeStamp: currentTimeStamp,
            headers: headers
            );
        GeneralResponseChecker(generalResponse);
        var responseList = generalResponse.data.list;
        return responseList;
    }

    public async Task<bool?> CancelOpenOrder(
        string symbol,
        string? orderId = null,
        string? orderLinkId = null
    )
    {
        // https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-dv_placeorder

        var endPoint = "/private/v1/trade/cancel";

        var query = new Dictionary<string, object>{
            {"symbol", symbol},
        };

        ExchangeUtils.AddOptionalParameters(
            query,
            ("orderId", orderId),
            ("orderLinkId", orderLinkId)
        );

        var currentTimeStamp = ExchangeUtils.GetCurrentTimeStampString();
        var headers = BuildRequestHeaders(currentTimeStamp, true);

        var generalResponse = await _apiService.SendSignedPostAsync<MufexGeneralResponse<MufexOrderData>>(
            endPoint: endPoint,
            query: query,
            signatureHeaderName: this.SignatureHeaderName,
            currentTimeStamp: currentTimeStamp,
            headers: headers
            );
        GeneralResponseChecker(generalResponse);
        var result = generalResponse.data.orderId == orderId;
        return result;
    }
    public async Task<List<MufexAllSymbolInfoData>?> GetAllSymbolsInfo(
        string category = "linear",
        string? symbol = null,
        int? limit = null)
    {
        // https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-dv_instrhead

        var endpoint = "/public/v1/instruments";

        var query = new Dictionary<string, object>{
            { "category", category }
        };
        ExchangeUtils.AddOptionalParameters(
            query,
            ("symbol", symbol),
            ("limit", limit)
        );

        var generalResponse = await _apiService.SendGetAsync<MufexGeneralResponse<MufexAllSymbolInfoDataList>>(
            endPoint: endpoint,
            query: query
            );
        GeneralResponseChecker(generalResponse);
        var responseList = generalResponse.data.list;
        return responseList;
    }

    public async Task<List<string>> GetListOfSymbols()
    {
        var symbols = new List<string>();

        var result = await this.GetAllSymbolsInfo();
        foreach (var symbol in result)
        {
            symbols.Add(symbol.symbol);
        }
        symbols.Sort();
        return symbols;
    }

    public async Task<string?> PlaceOrder(
        string symbol,
        MufexSide side,
        MufexPositionIndex positionIdx,
        MufexOrderType orderType,
        decimal qty,
        MufexTimeInForce timeInForce,
        decimal? price = null,
        MufexTriggerDirection? triggerDirection = null,
        decimal? triggerPrice = null,
        MufexTriggerBy? triggerBy = null,
        MufexTriggerBy? tpTriggerBy = null,
        MufexTriggerBy? slTriggerBy = null,
        string? orderLinkId = null,
        decimal? takeProfit = null,
        decimal? stopLoss = null,
        bool? reduceOnly = null,
        bool? closeOnTrigger = null)
    {
        // https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-dv_placeorder

        var endPoint = "/private/v1/trade/create";

        var query = new Dictionary<string, object>
                        {
                            { "symbol", symbol },
                            { "side", side.Value },
                            { "orderType", orderType.Value },
                            { "qty", qty.ToString()},
                            { "positionIdx", positionIdx.Value },
                            { "timeInForce", timeInForce.Value }
                        };

        ExchangeUtils.AddOptionalParameters(query,
            ("price", price.ToString()),
            ("triggerDirection", triggerDirection),
            ("triggerPrice", triggerPrice.ToString()),
            ("triggerBy", triggerBy?.Value),
            ("orderLinkId", orderLinkId),
            ("takeProfit", takeProfit.ToString()),
            ("stopLoss", stopLoss.ToString()),
            ("tpTriggerBy", tpTriggerBy?.Value),
            ("slTriggerBy", slTriggerBy?.Value),
            ("reduceOnly", reduceOnly),
            ("closeOnTrigger", closeOnTrigger)
        );

        var currentTimeStamp = ExchangeUtils.GetCurrentTimeStampString();
        var headers = BuildRequestHeaders(currentTimeStamp, true);


        var generalResponse = await _apiService.SendSignedPostAsync<MufexGeneralResponse<MufexOrderData>>(
            endPoint: endPoint,
            query: query,
            signatureHeaderName: this.SignatureHeaderName,
            currentTimeStamp: currentTimeStamp,
            headers: headers
            );
        GeneralResponseChecker(generalResponse);
        var orderId = generalResponse.data.orderId;
        return orderId;
    }
}