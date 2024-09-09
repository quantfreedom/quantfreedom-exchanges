using Exchanges.Services.Interfaces;
using Exchanges.Utils;
using Exchnages.Utils;
using Microsoft.Extensions.Configuration;
using Models.Mufex;
using Newtonsoft.Json;

namespace Exchanges.Mufex;

public class Mufex : Exchange
{
    private readonly IConfiguration _configuration;

    public Mufex(
        IConfiguration configuration,
        IApiService apiService
    ) : base(apiService, "https://api.mufex.finance", "MF-ACCESS-SIGN")
    {
        _configuration = configuration;
    }
    protected override Dictionary<string, string> BuildRequestHeaders(string currentTimeStamp, bool includeApiKey)
    {
        var apiKey = _configuration["Exchange:ApiKey"];
        var recvWindow = _configuration["RecvWindow"];

        var headers = new Dictionary<string, string>
        {
            { "MF-ACCESS-SIGN-TYPE", ExchangeConstants.DEFAULT_SIGN_TYPE },
            { "MF-ACCESS-TIMESTAMP", currentTimeStamp },
            { "MF-ACCESS-RECV-WINDOW", recvWindow },
            { "X-Referer", "AKF3CWKDT" }
        };

        if (includeApiKey)
        {
            headers.Add("MF-ACCESS-API-KEY", apiKey);
        }
        return headers;
    }

    public async Task<List<GetWalletBalanceDataList>?> GetAccountBalance(string? coin = null)
    {
        var query = new Dictionary<string, object>();

        ExchangeUtils.AddOptionalParameters(
            query,
            ("coin", coin)
        );

        var response = await this.GetAccountBalance<string>(
            requestUrl: "/private/v1/account/balance",
            query: query
            );
        var generalResponse = JsonConvert.DeserializeObject<GeneralResponse<GetWalletBalanceData>>(response);
        var responseList = generalResponse.data.list;
        return responseList;
    }
}
