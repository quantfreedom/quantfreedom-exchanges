using mufex.net.api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using mufex.net.api.Utils;

namespace mufex.net.api.Services;

public class MufexAdjustersService
{
    private readonly MufexApiService apiService;
    public MufexAdjustersService(
        MufexApiService apiService
        )
    {
        this.apiService = apiService;
    }
    /// <summary>
    /// https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-contract_replaceorder
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="orderId"></param>
    /// <param name="orderLinkID"></param>
    /// <param name="qty"></param>
    /// <param name="price"></param>
    /// <param name="triggerPrice"></param>
    /// <returns></returns>
    public async Task<string?> AdjustOrder(
        string symbol,
        string? orderId = null,
        string? orderLinkID = null,
        double? qty = null,
        double? price = null,
        double? triggerPrice = null
    )
    {
        var query = new Dictionary<string, object>{
            { "symbol", symbol }
        };

        MufexUtils.AddOptionalParameters(
            query,
            ("orderId", orderId),
            ("orderLinkID", orderLinkID),
            ("qty", qty.ToString()),
            ("price", price.ToString()),
            ("triggerPrice", triggerPrice.ToString())
        );

        var result = await this.apiService.SendSignedAsync<string>(
            requestUri: "/private/v1/trade/replace",
            httpMethod: HttpMethod.Post,
            query: query
            );
        var jsonResult = JsonConvert.DeserializeObject<GeneralResponse<CreateOrderData>>(result);
        var jsonResultList = jsonResult.data;
        string jsonFormatted = JValue.Parse(result).ToString(Formatting.Indented);
        Console.WriteLine(jsonFormatted);
        return result;
    }
}
