using Exchanges.Exceptions;
using Exchanges.Services.Interfaces;
using Exchnages.Utils;
using Models.Bybit;
using Newtonsoft.Json;

namespace Exchanges.Bybit;

public class Bybit
{
    private readonly IApiService _apiService;
    private readonly string SignatureHeaderName = "X-BAPI-SIGN";
    public readonly string TestnetURL = "https://api-testnet.bybit.com";
    public readonly string MainnetURL = "https://api.bybit.com";
    public Bybit(
        IApiService apiService,
        string apiKey,
        string secretKey,
        bool useTestnet
    )
    {
        _apiService = apiService;
        _apiService.ApiKey = apiKey;
        _apiService.SecretKey = secretKey;
        _apiService.BaseUrl = useTestnet ? TestnetURL : MainnetURL;
    }
    public Bybit(IApiService apiService, bool useTestnet)
    {
        _apiService = apiService;
        _apiService.BaseUrl = useTestnet ? TestnetURL : MainnetURL;
    }

    private void BybitGeneralResponseChecker<T>(BybitGeneralResponse<T> generalResponse)
    {
        var code = generalResponse.code;
        var message = generalResponse.message;
        if (code != 0 || message.ToLower() != "ok")
        {
            throw new ExchangeClientException(message: message, code: code);
        }
    }
    // https://www.Bybit.finance/apidocs/derivatives/contract/index.html#t-balance
    private Dictionary<string, string> BuildRequestHeaders(string currentTimeStamp, bool includeApiKey)
    {
        var headers = new Dictionary<string, string>
        {
            { "X-BAPI-SIGN-TYPE", "2" },
            { "X-BAPI-TIMESTAMP", currentTimeStamp },
            { "X-BAPI-RECV-WINDOW", "5000" },
            { "X-BAPI-API-KEY", _apiService.ApiKey },
            { "referer", "Rx000377" }
        };
        return headers;
    }

    public async Task<List<BybitWalletBalanceDataList>?> GetAccountBalance(
        BybitAccountType accountType,
        string? coin = null
        )
    {
        // https://bybit-exchange.github.io/docs/v5/account/wallet-balance

        var endpoint = "/v5/account/wallet-balance";

        var query = new Dictionary<string, object>{
            { "accountType", accountType.Value }
        };

        ExchangeUtils.AddOptionalParameters(
            query,
            ("coin", coin)
        );

        var currentTimeStamp = ExchangeUtils.GetCurrentTimeStampString();
        var headers = BuildRequestHeaders(currentTimeStamp, true);

        var generalResponse = await _apiService.SendSignedGetAsync<BybitGeneralResponse<BybitWalletBalanceData>>(
            endPoint: endpoint,
            query: query,
            signatureHeaderName: this.SignatureHeaderName,
            currentTimeStamp: currentTimeStamp,
            headers: headers
            );
        BybitGeneralResponseChecker(generalResponse);
        var responseList = generalResponse.data.list;
        return responseList;
    }
    public async Task<List<BybitOpenAndClosedOrdersData>?> GetOpenAndClosedOrders(
        BybitCategoryType category,
        string? symbol = null,
        string? baseCoin = null,
        string? settleCoin = null,
        string? orderId = null,
        string? orderLinkId = null,
        bool? openOnly = null,
        BybitOrderFilterType orderFilter = null,
        int? limit = null
    )
    {
        // https://bybit-exchange.github.io/docs/v5/order/open-order
        var endpoint = "/v5/order/realtime";

        var query = new Dictionary<string, object>{
            { "category", category.Value}
        };

        ExchangeUtils.AddOptionalParameters(
            query,
            ("symbol", symbol),
            ("baseCoin", baseCoin),
            ("settleCoin", settleCoin),
            ("orderId", orderId),
            ("orderLinkId", orderLinkId),
            ("openOnly", openOnly),
            ("orderFilter", orderFilter?.Value),
            ("limit", limit)
        );

        var currentTimeStamp = ExchangeUtils.GetCurrentTimeStampString();
        var headers = BuildRequestHeaders(currentTimeStamp, true);

        var generalResponse = await _apiService.SendSignedGetAsync<BybitGeneralResponse<BybitOpenAndClosedOrdersDataList>>(
            endPoint: endpoint,
            query: query,
            signatureHeaderName: this.SignatureHeaderName,
            currentTimeStamp: currentTimeStamp,
            headers: headers
            );
        BybitGeneralResponseChecker(generalResponse);
        var responseList = generalResponse.data.list;
        return responseList;
    }
    public async Task<List<BybitOpenAndClosedOrdersData>?> GetOpenOrders(
        BybitCategoryType category,
        string? symbol = null,
        string? baseCoin = null,
        string? settleCoin = null,
        string? orderId = null,
        string? orderLinkId = null,
        BybitOrderFilterType orderFilter = null,
        int? limit = null
    )
    {
        var result = await this.GetOpenAndClosedOrders(
            category: category,
            symbol: symbol,
            baseCoin: baseCoin,
            settleCoin: settleCoin,
            orderId: orderId,
            orderLinkId: orderLinkId,
            openOnly: true,
            orderFilter: orderFilter,
            limit: limit
        );
        return result;
    }

    public async Task<bool?> CancelOpenOrder(
        BybitCategoryType category,
        string symbol,
        string? orderId = null,
        string? orderLinkId = null,
        BybitOrderFilterType orderFilter = null
    )
    // https://bybit-exchange.github.io/docs/v5/order/cancel-order
    {
        var endPoint = "/v5/order/cancel";

        var query = new Dictionary<string, object>{
            {"category", category.Value},
            {"symbol", symbol}
        };

        ExchangeUtils.AddOptionalParameters(
            query,
            ("orderId", orderId),
            ("orderLinkId", orderLinkId),
            ("orderFilter", orderFilter?.Value)
        );

        var currentTimeStamp = ExchangeUtils.GetCurrentTimeStampString();
        var headers = BuildRequestHeaders(currentTimeStamp, true);

        var generalResponse = await _apiService.SendSignedPostAsync<BybitGeneralResponse<BybitOrderData>>(
            endPoint: endPoint,
            query: query,
            signatureHeaderName: this.SignatureHeaderName,
            currentTimeStamp: currentTimeStamp,
            headers: headers
            );
        BybitGeneralResponseChecker(generalResponse);
        var result = generalResponse.data.orderId == orderId;
        return result;
    }
    public async Task<List<BybitAllSymbolInfoData>> GetAllSymbolsInfo(
        BybitCategoryType category,
        string? symbol = null,
        string? baseCoin = null,
        int? limit = null
    )
    {
        // https://bybit-exchange.github.io/docs/v5/market/instrument

        var endpoint = "/v5/market/instruments-info";

        var query = new Dictionary<string, object>{
            { "category", category }
        };
        ExchangeUtils.AddOptionalParameters(
            query,
            ("symbol", symbol),
            ("baseCoin", baseCoin),
            ("limit", limit)
        );

        var generalResponse = await _apiService.SendGetAsync<BybitGeneralResponse<BybitAllSymbolInfoDataList>>(
            endPoint: endpoint,
            query: query
            );
        BybitGeneralResponseChecker(generalResponse);
        var responseList = generalResponse.data.list;
        return responseList;
    }

    public async Task<List<string>> GetListOfSymbols(BybitCategoryType category)
    {
        var symbols = new List<string>();

        var result = await this.GetAllSymbolsInfo(category: category);
        foreach (var symbol in result)
        {
            symbols.Add(symbol.symbol);
        }
        symbols.Sort();
        return symbols;
    }


    public async Task<string?> PlaceOrder(
        BybitCategoryType category,
        string symbol,
        BybitSide side,
        BybitOrderType orderType,
        decimal qty,

        int? isLeverage = null,
        string? marketUnit = null,
        decimal? price = null,
        BybitTriggerDirection? triggerDirection = null,
        BybitOrderFilterType? orderFilter = null,
        decimal? triggerPrice = null,
        BybitTriggerBy? triggerBy = null,
        string? orderIv = null,
        BybitTimeInForce? timeInForce = null,
        BybitPositionIndex? positionIdx = null,
        string? orderLinkId = null,
        decimal? takeProfit = null,
        decimal? stopLoss = null,
        BybitTriggerBy? tpTriggerBy = null,
        BybitTriggerBy? slTriggerBy = null,
        bool? reduceOnly = null,
        bool? closeOnTrigger = null,
        string? smpType = null,
        bool? mmp = null,
        string? tpslMode = null,
        string? tpLimitrice = null,
        string? slLimitPrice = null,
        string? tpOrderType = null,
        string? slOrderType = null
    )
    {
        // https://bybit-exchange.github.io/docs/v5/order/create-order

        var endPoint = "/v5/order/create";

        var query = new Dictionary<string, object>
                        {
                            { "category", category.Value },
                            { "symbol", symbol },
                            { "side", side.Value },
                            { "orderType", orderType.Value },
                            { "qty", qty.ToString()},
                        };

        ExchangeUtils.AddOptionalParameters(query,
            ("price", price.ToString()),
            ("triggerDirection", triggerDirection?.Value),
            ("triggerPrice", triggerPrice.ToString()),
            ("triggerBy", triggerBy?.Value),
            ("orderLinkId", orderLinkId),
            ("takeProfit", takeProfit.ToString()),
            ("stopLoss", stopLoss.ToString()),
            ("tpTriggerBy", tpTriggerBy?.Value),
            ("slTriggerBy", slTriggerBy?.Value),
            ("reduceOnly", reduceOnly),
            ("closeOnTrigger", closeOnTrigger),
            ("isLeverage", isLeverage),
            ("positionIdx", positionIdx?.Value),
            ("timeInForce", timeInForce?.Value),
            ("marketUnit", marketUnit),
            ("orderIv", orderIv),
            ("orderFilter", orderFilter?.Value),
            ("smpType", smpType),
            ("mmp", mmp),
            ("tpslMode", tpslMode),
            ("tpLimitPrice", tpLimitrice),
            ("slLimitPrice", slLimitPrice),
            ("tpOrderType", tpOrderType),
            ("slOrderType", slOrderType)
        );

        var currentTimeStamp = ExchangeUtils.GetCurrentTimeStampString();
        var headers = BuildRequestHeaders(currentTimeStamp, true);


        var generalResponse = await _apiService.SendSignedPostAsync<BybitGeneralResponse<BybitOrderData>>(
            endPoint: endPoint,
            query: query,
            signatureHeaderName: this.SignatureHeaderName,
            currentTimeStamp: currentTimeStamp,
            headers: headers
            );
        BybitGeneralResponseChecker(generalResponse);
        var orderId = generalResponse.data.orderId;
        return orderId;
    }
}

