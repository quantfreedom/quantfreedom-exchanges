using System.Security.Cryptography;
using System.Text;
using Exchanges.Exceptions;
using Exchanges.Utils;
using Exchnages.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Exchanges;
public abstract class Exchange
{
    public readonly string UserAgent = "QuantFreedom" + VersionInfo.GetVersion;
    public readonly string? ApiKey;
    public readonly string? SecretKey;
    public readonly string? BaseUrl;
    public readonly HttpClient httpClient;
    public readonly bool DebugMode;
    public readonly string RecvWindow;
    public string CurrentTimeStamp;
    protected abstract HttpRequestMessage BuildHttpRequest(
        string requestUrl,
        HttpMethod httpMethod,
        string? signature,
        string? postString
        );
        
    public Exchange(
        string? apiKey = null,
        string? secretKey = null,
        string? baseUrl = null,
        bool debugMode = false,
        string recvWindow = ExchangeConstants.DEFAULT_REC_WINDOW
    )
    {
        this.httpClient = new HttpClient();
        this.ApiKey = apiKey;
        this.SecretKey = secretKey;
        this.BaseUrl = baseUrl;
        this.DebugMode = debugMode;
        this.RecvWindow = recvWindow;
        this.CurrentTimeStamp = string.Empty;
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
    public async Task<T?> SendSignedAsync<T>(
        string requestUrl,
        HttpMethod httpMethod,
        Dictionary<string, object>? query = null
    )
    {
        string? queryString = string.Empty;
        string? postString = null;
        string? signature = null;
        this.CurrentTimeStamp = ExchangeUtils.GetCurrentTimeStampString();

        if (httpMethod == HttpMethod.Get)
        {
            if (query is not null)
            {
                queryString = GenerateQueryString(query);
            }
            requestUrl = queryString.Length > 0 ? requestUrl + "?" + queryString : requestUrl;
        }
        else if (httpMethod == HttpMethod.Post)
        {
            if (query is not null)
            {
                queryString = JsonConvert.SerializeObject(query);
                postString = queryString;
            }
        }
        string rawData = this.CurrentTimeStamp + this.ApiKey + this.RecvWindow + queryString;
        try
        {
            if (string.IsNullOrEmpty(this.ApiKey) || string.IsNullOrEmpty(this.SecretKey))
                throw new Exception("Please set your api key and api secret");
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(this.SecretKey));
            byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            signature = BitConverter.ToString(computedHash).Replace("-", "").ToLower();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to calculate HMAC-SHA256 ", ex);
        }

        var response = await SendAsync<T>(
            requestUrl: requestUrl,
            httpMethod: httpMethod,
            signature: signature,
            postString: postString
            );

        return response;

    }
    private string GenerateQueryString(IDictionary<string, object> parameters)
    {
        var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={p.Value}"));
        return queryString;
    }
    private async Task<T?> SendAsync<T>(
    string requestUrl,
    HttpMethod httpMethod,
    string? signature = null,
    string? postString = null
    )
    {
        HttpRequestMessage request = BuildHttpRequest(requestUrl, httpMethod, signature, postString);

        LogHttpRequestHeader(request);

        HttpResponseMessage response = await this.httpClient.SendAsync(request);

        LogHttpResponseHeader(response);

        using HttpContent responseContent = response.Content;
        string contentString = await responseContent.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            if (typeof(T) == typeof(string))
            {
                return (T)(object)contentString;
            }
            else
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(contentString);
                }
                catch (JsonReaderException ex)
                {
                    var clientException = new ExchangeClientException($"Failed to map server response from '{requestUrl}' to given type", -1, ex)
                    {
                        StatusCode = (int)response.StatusCode,
                        Headers = response.Headers.ToDictionary(a => a.Key, a => a.Value)
                    };

                    throw clientException;
                }
            }
        }
        else
        {
            ExchangeHttpException? httpException;
            int statusCode = (int)response.StatusCode;
            if (!string.IsNullOrWhiteSpace(contentString))
            {
                try
                {
                    httpException = JsonConvert.DeserializeObject<ExchangeClientException>(contentString);
                }
                catch (JsonReaderException ex)
                {
                    httpException = new ExchangeClientException(contentString, -1, ex);
                }
            }
            else
            {
                httpException = statusCode >= 400 && statusCode < 500 ? new ExchangeClientException("Unsuccessful response with no content", -1) : new ExchangeClientException(contentString);
            }

            if (httpException != null) // Check for null before dereferencing
            {
                httpException.StatusCode = statusCode;
                httpException.Headers = response.Headers.ToDictionary(a => a.Key, a => a.Value);

                throw httpException;
            }
        }
        return default;
    }
    private void LogHttpResponseHeader(HttpResponseMessage response)
    {
        if (this.DebugMode)
        {
            Console.WriteLine("--------------------HTTP Response Headers:-----------------------");
            foreach (var header in response.Headers)
            {
                Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }
        }
    }

    private void LogHttpRequestHeader(HttpRequestMessage request)
    {
        if (this.DebugMode)
        {
            Console.WriteLine("--------------------HTTP Request Headers:------------------------");
            foreach (var header in request.Headers)
            {
                Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }
        }
    }

    
}
