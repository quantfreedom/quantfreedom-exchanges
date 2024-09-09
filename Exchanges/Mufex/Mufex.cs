using System.Security.Cryptography;
using Exchanges.Services.Interfaces;
using Exchanges.Utils;
using Exchnages.Utils;
using Microsoft.Extensions.Configuration;
using Models.Mufex;
using Newtonsoft.Json;

namespace Exchanges.Mufex;

public class Mufex
{
    private readonly IConfiguration _configuration;
    private readonly IApiService _apiService;
    public Mufex(
        IConfiguration configuration,
        IApiService apiService
    )
    {
        _configuration = configuration;
        _apiService = apiService;
    }
    private Dictionary<string, string> BuildRequestHeaders(string currentTimeStamp, bool includeApiKey)
    {
        var headers = new Dictionary<string, string>
        {
            { "MF-ACCESS-SIGN-TYPE", ExchangeConstants.DEFAULT_SIGN_TYPE },
            { "MF-ACCESS-TIMESTAMP", currentTimeStamp },
            { "MF-ACCESS-RECV-WINDOW", _configuration["RecvWindow"] },
            { "X-Referer", "AKF3CWKDT" }
        };

        if (includeApiKey)
        {
            headers.Add("MF-ACCESS-API-KEY", _configuration["Exchange:ApiKey"]);
        }
        return headers;
    }

    public async Task<List<GetWalletBalanceDataList>?> GetAccountBalance(string? coin = null)
    {
        var endpoint = "/private/v1/account/balance";

        var query = new Dictionary<string, object>();

        ExchangeUtils.AddOptionalParameters(
            query,
            ("coin", coin)
        );

        var currentTimeStamp = ExchangeUtils.GetCurrentTimeStampString();
        var headers = BuildRequestHeaders(currentTimeStamp, true);

        var response = await _apiService.SendSignedGetAsync<string>(
            requestUrl: endpoint,
            query: query,
            signatureHeaderName: _configuration["Exchange:SignatureHeaderName"],
            currentTimeStamp: currentTimeStamp,
            headers: headers
            );
        var generalResponse = JsonConvert.DeserializeObject<GeneralResponse<GetWalletBalanceData>>(response);
        var responseList = generalResponse.data.list;
        return responseList;
    }
}
