using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using mufex.net.api.Models;

namespace mufex.net.api.Services;
public class MufexPlacersService
{
    private readonly MufexApiService apiService;
    public MufexPlacersService(
        MufexApiService apiService
        )
    {
        this.apiService = apiService;
    }

    /// <summary>
    /// Send in a new order.
    /// - Supported order types: Limit order (specify order qty and price), Market order (execute at best available market price, with price conversion protections).
    /// - Supported timeInForce strategies: GTC, IOC, FOK, PostOnly (higher quantity limits, consult instruments-info endpoint).
    /// - Conditional orders: Converted when triggerPrice is set; does not occupy margin, may be canceled if margin is insufficient post-trigger.
    /// - Take Profit/Stop Loss: Can be set during order placement and modified for positions.
    /// - Order quantity for perpetual contracts must be positive.
    /// - Limit order price: Must be above liquidation price, consult instruments-info endpoint for minimum changes.
    /// - Custom order IDs (orderLinkId) can be used for linking to system IDs; prioritize orderId over orderLinkId if both provided.
    /// - Order limits: Futures (500 active orders per contract), Spot (500 total, 30 TP/SL, 30 conditional), Option (100 open).
    /// - Rate limits and risk control limits apply. Excessive API requests may trigger restrictions.
    /// https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-dv_placeorder
    /// TIP: Borrow margin for margin trading on spot in a normal account.
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="side"></param>
    /// <param name="orderType"></param>
    /// <param name="qty"></param>
    /// <param name="price"></param>
    /// <param name="timeInForce"></param>
    /// <param name="triggerDirection"></param>
    /// <param name="triggerPrice"></param>
    /// <param name="triggerBy"></param>
    /// <param name="positionIdx"></param>
    /// <param name="orderLinkId"></param>
    /// <param name="takeProfit"></param>
    /// <param name="stopLoss"></param>
    /// <param name="tpTriggerBy"></param>
    /// <param name="slTriggerBy"></param>
    /// <param name="reduceOnly"></param>
    /// <param name="closeOnTrigger"></param>
    /// <returns>Order result</returns>
    public async Task<string?> PlaceOrder(
        string symbol,
        Side side,
        PositionIndex positionIdx,
        OrderType orderType,
        double qty,
        TimeInForce timeInForce,

        double? price = null,
        TriggerDirection? triggerDirection = null,
        double? triggerPrice = null,
        TriggerBy? triggerBy = null,
        TriggerBy? tpTriggerBy = null,
        TriggerBy? slTriggerBy = null,
        string? orderLinkId = null,
        double? takeProfit = null,
        double? stopLoss = null,
        bool? reduceOnly = null,
        bool? closeOnTrigger = null
        )
    {
        var query = new Dictionary<string, object>
                        {
                            { "symbol", symbol },
                            { "side", side.Value },
                            { "orderType", orderType.Value },
                            { "qty", qty.ToString()},
                            { "positionIdx", positionIdx.Value },
                            { "timeInForce", timeInForce.Value }
                        };

        MufexParametersUtils.AddOptionalParameters(query,
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
        var result = await apiService.SendSignedAsync<string>(
            requestUri: "/private/v1/trade/create",
            httpMethod: HttpMethod.Post,
            query: query
            );
        var jsonResult = JsonConvert.DeserializeObject<GeneralResponse<CreateOrderData>>(result);
        var orderId = jsonResult.data.orderId;
        return orderId;
    }
}
