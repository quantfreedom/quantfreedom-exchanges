using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using Exchanges.Exceptions;
using Exchanges.Utils;
using Exchnages.Utils;
using Newtonsoft.Json;

namespace Exchanges;
public abstract class Exchange
{
    public readonly string? BaseUrl;
    protected abstract HttpRequestHeaders BuildHttpRequest(
        HttpMethod httpMethod
        );
        
    public Exchange(
        string baseUrl,
        string recvWindow = ExchangeConstants.DEFAULT_REC_WINDOW
    )
    {
        this.BaseUrl = baseUrl;
    }


    public async Task<string> CallRequest(
        Dictionary<string, object> query,
        string requestUrl,
        HttpMethod httpMethod
        )
    {
        var response = await SendSignedAsync<string>(
                requestUrl: requestUrl,
                httpMethod: HttpMethod.Get,
                query: query
                );
        return response;
    }
    
    private string GenerateQueryString(IDictionary<string, object> parameters)
    {
        var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={p.Value}"));
        return queryString;
    }
}
