using Exchanges.Services.Interfaces;
using Exchnages.Utils;

namespace Exchanges;

public abstract class Exchange
{
    public readonly string BaseUrl;
    public readonly string SignatureHeaderName;
    private readonly IApiService _apiService;
    protected abstract Dictionary<string, string> BuildRequestHeaders(string currentTimeStamp, bool includeApiKey);
        
    public Exchange(
        IApiService apiService,
        string baseUrl,
        string signatureHeaderName
    )
    {
        this.BaseUrl = baseUrl;
        this.SignatureHeaderName = signatureHeaderName;
        _apiService = apiService;
    }


    public async Task<T> GetAccountBalance<T>(
        Dictionary<string, object> query,
        string requestUrl
        )
    {
        var currentTimeStamp = ExchangeUtils.GetCurrentTimeStampString();
        var headers = BuildRequestHeaders(currentTimeStamp, true);
        var response = await _apiService.SendSignedGetAsync<T>(
                requestUrl: BaseUrl.TrimEnd('/') + '/' + requestUrl.TrimStart('/'),
                currentTimeStamp: currentTimeStamp,
                signatureHeaderName: SignatureHeaderName,
                headers: headers,
                query: query
                );
        return response;
    }
}
