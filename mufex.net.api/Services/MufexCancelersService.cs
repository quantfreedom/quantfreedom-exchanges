using mufex.net.api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using mufex.net.api.Utils;
namespace mufex.net.api.Services;

public class MufexCancelersService
{
    private readonly MufexApiService apiService;
    public MufexCancelersService(
        MufexApiService apiService
        )
    {
        this.apiService = apiService;
    }

    public async Task<string?> CancelOpenOrder(
        string symbol,
        string? orderId = null,
        string? orderLinkId = null
    )
    {
        var query = new Dictionary<string, object>{
            {"symbol", symbol},
        };

        ExchangeUtils.AddOptionalParameters(
            query,
            ("orderId", orderId),
            ("orderLinkId", orderLinkId)
        );

        var result = await this.apiService.SendSignedAsync<string>(
            requestUrl: "/private/v1/trade/cancel",
            httpMethod: HttpMethod.Post,
            query: query
            );
        var jsonResult = JsonConvert.DeserializeObject<GeneralResponse<OrderData>>(result);
        var jsonResultList = jsonResult.data;
        string jsonFormatted = JValue.Parse(result).ToString(Formatting.Indented);
        Console.WriteLine(jsonFormatted);
        return result;
    }
    public async Task<string?> CancelAllOpenOrders(
        string symbol
    )
    {
        var query = new Dictionary<string, object>{
            {"symbol", symbol},
        };

        var result = await this.apiService.SendSignedAsync<string>(
            requestUrl: "/private/v1/trade/cancel-all",
            httpMethod: HttpMethod.Post,
            query: query
            );
        var jsonResult = JsonConvert.DeserializeObject<GeneralResponse<OrderData>>(result);
        var jsonResultList = jsonResult.data;
        string jsonFormatted = JValue.Parse(result).ToString(Formatting.Indented);
        Console.WriteLine(jsonFormatted);
        return result;
    }
}
