using mufex.net.api.Models;
using mufex.net.api.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace mufex.net.api.Services;

public class MufexSettersService
{
    private readonly MufexApiService apiService;
    public MufexSettersService(
        MufexApiService apiService
        )
    {
        this.apiService = apiService;
    }

    /// <summary>
    /// https://www.mufex.finance/apidocs/derivatives/contract/index.html?console#t-linear_account_para_setleverage
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="buyLeverage"></param>
    /// <param name="sellLeverage"></param>
    /// <returns></returns>
    public async Task<string?> SetLeverage(
        string symbol,
        double buyLeverage,
        double sellLeverage
        )
    {
        var query = new Dictionary<string, object>{
            { "symbol", symbol },
            { "buyLeverage", buyLeverage.ToString() },
            { "sellLeverage", sellLeverage.ToString() }
        };


        var result = await this.apiService.SendSignedAsync<string>(
                requestUrl: "/private/v1/account/set-leverage",
                httpMethod: HttpMethod.Post,
                query: query
        );

        var jsonResult = JsonConvert.DeserializeObject<GeneralResponse<Dictionary<string, object>>>(result);
        var jsonResultData = jsonResult.data;
        string jsonFormatted = JValue.Parse(result).ToString(Formatting.Indented);
        Console.WriteLine(jsonFormatted);
        return result;
    }

    /// <summary>
    /// https://www.mufex.finance/apidocs/derivatives/contract/index.html?console#t-dv_marginswitch
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="tradeMode"></param>
    /// <param name="buyLeverage"></param>
    /// <param name="sellLeverage"></param>
    /// <returns></returns>
    public async Task<string?> SetLeverageMode(
        string symbol,
        LeverageMode leverageMode,
        double? buyLeverage = null,
        double? sellLeverage = null
        )
    {
        var query = new Dictionary<string, object>{
            { "symbol", symbol },
            { "tradeMode", leverageMode.Value }
        };

        MufexUtils.AddOptionalParameters(
            query,
            ("buyLeverage", buyLeverage.ToString()),
            ("sellLeverage", sellLeverage.ToString())
        );


        var result = await this.apiService.SendSignedAsync<string>(
                requestUrl: "/private/v1/account/set-isolated",
                httpMethod: HttpMethod.Post,
                query: query
        );

        var jsonResult = JsonConvert.DeserializeObject<GeneralResponse<Dictionary<string, object>>>(result);
        var jsonResultData = jsonResult.data;
        string jsonFormatted = JValue.Parse(result).ToString(Formatting.Indented);
        Console.WriteLine(jsonFormatted);
        return result;
    }


}
