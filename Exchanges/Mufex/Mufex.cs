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
    private readonly IApiService _apiService;
    private readonly string SignatureHeaderName = "MF-ACCESS-SIGN";
    public readonly string TestnetURL = "https://api.testnet.mufex.finance";
    public readonly string MainnetURL = "https://api.mufex.finance";
    public Mufex(
        IApiService apiService,
        string apiKey,
        string secretKey,
        bool useTestnet = false
    )
    {
        _apiService = apiService;
        _apiService.ApiKey = apiKey;
        _apiService.SecretKey = secretKey;
        _apiService.BaseUrl = useTestnet ? TestnetURL : MainnetURL;

    }
    private Dictionary<string, string> BuildRequestHeaders(string currentTimeStamp, bool includeApiKey)
    {
        var headers = new Dictionary<string, string>
        {
            { "MF-ACCESS-SIGN-TYPE", ExchangeConstants.DEFAULT_SIGN_TYPE },
            { "MF-ACCESS-TIMESTAMP", currentTimeStamp },
            { "MF-ACCESS-RECV-WINDOW", "5000" },
            { "X-Referer", "AKF3CWKDT" }
        };

        if (includeApiKey)
        {
            headers.Add("MF-ACCESS-API-KEY", _apiService.ApiKey);
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
            endPoint: endpoint,
            query: query,
            signatureHeaderName: this.SignatureHeaderName,
            currentTimeStamp: currentTimeStamp,
            headers: headers
            );
        var generalResponse = JsonConvert.DeserializeObject<GeneralResponse<GetWalletBalanceData>>(response);
        var responseList = generalResponse.data.list;
        return responseList;
    }
}
