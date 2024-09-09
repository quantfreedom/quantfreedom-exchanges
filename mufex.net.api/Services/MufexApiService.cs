using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using mufex.net.api.Utils;

namespace mufex.net.api.Services;

public class MufexApiService
{
    private static readonly string UserAgent = "mufex.net.api/" + VersionInfo.GetVersion;
    private readonly string? apiKey;
    private readonly string? apiSecret;
    private readonly string? baseUrl;
    private readonly HttpClient httpClient;
    private readonly bool debugMode;
    private readonly string recvWindow;
    private string CurrentTimeStamp;

    public MufexApiService(
        string? apiKey = null,
        string? apiSecret = null,
        string? baseUrl = null,
        bool debugMode = false,
        string recvWindow = MufexConstants.DEFAULT_REC_WINDOW
    )
    {
        this.httpClient = new HttpClient();
        this.apiKey = apiKey;
        this.apiSecret = apiSecret;
        this.baseUrl = baseUrl;
        this.debugMode = debugMode;
        this.recvWindow = recvWindow;
        this.CurrentTimeStamp = string.Empty;
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
        this.CurrentTimeStamp = MufexUtils.GetCurrentTimeStampString();

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
        string rawData = this.CurrentTimeStamp + apiKey + this.recvWindow + queryString;
        try
        {
            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
                throw new Exception("Please set your api key and api secret");
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(apiSecret));
            byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            signature = BitConverter.ToString(computedHash).Replace("-", "").ToLower();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to calculate HMAC-SHA256 ", ex);
        }

        var result = await SendAsync<T>(
            requestUrl: requestUrl,
            httpMethod: httpMethod,
            signature: signature,
            postString: postString
            );

        return result;

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
        string jsonFormatted = JValue.Parse(contentString).ToString(Formatting.Indented);
        // var testing = JsonConvert.DeserializeObject<GeneralResponse<GetWalletData>>(contentString);
        // Console.WriteLine(jsonFormatted);
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
                    var clientException = new MufexClientException($"Failed to map server response from '{requestUrl}' to given type", -1, ex)
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
            MufexHttpException? httpException;
            int statusCode = (int)response.StatusCode;
            if (!string.IsNullOrWhiteSpace(contentString))
            {
                try
                {
                    httpException = JsonConvert.DeserializeObject<MufexClientException>(contentString);
                }
                catch (JsonReaderException ex)
                {
                    httpException = new MufexClientException(contentString, -1, ex);
                }
            }
            else
            {
                httpException = statusCode >= 400 && statusCode < 500 ? new MufexClientException("Unsuccessful response with no content", -1) : new MufexClientException(contentString);
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
        if (debugMode)
        {
            Console.WriteLine("--------------------HTTP Response Headers:-----------------------");
            foreach (var header in response.Headers)
            {
                Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }
        }
    }

    /// <summary>
    /// Log http request header in console when debug mode active
    /// </summary>
    /// <param name="request"></param>
    private void LogHttpRequestHeader(HttpRequestMessage request)
    {
        if (debugMode)
        {
            Console.WriteLine("--------------------HTTP Request Headers:------------------------");
            foreach (var header in request.Headers)
            {
                Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }
        }
    }

    /// <summary>
    /// Build http request add attributes to header
    /// </summary>
    /// <param name="requestUrl">The URI of the endpoint to request.</param>
    /// <param name="httpMethod">The HTTP method (GET, POST, etc.) of the request.</param>
    /// <param name="signature">Optional signature for authentication.</param>
    /// <param name="content">Optional content to include in the request body.</param>
    /// <returns>Http Request message</returns>
    private HttpRequestMessage BuildHttpRequest(
        string requestUrl,
        HttpMethod httpMethod,
        string? signature,
        string? postString
        )
    {
        var theUrl = this.baseUrl + requestUrl;
        HttpRequestMessage request = new(httpMethod, theUrl);
        request.Headers.Add("User-Agent", UserAgent);
        if (signature != null && signature.Length > 0)
        {
            request.Headers.Add("MF-ACCESS-SIGN", signature);
        }
        request.Headers.Add("MF-ACCESS-SIGN-TYPE", MufexConstants.DEFAULT_SIGN_TYPE);
        request.Headers.Add("MF-ACCESS-TIMESTAMP", this.CurrentTimeStamp);
        request.Headers.Add("MF-ACCESS-RECV-WINDOW", recvWindow);
        request.Headers.Add("X-Referer", "AKF3CWKDT");
        if (this.apiKey is not null)
        {
            request.Headers.Add("MF-ACCESS-API-KEY", this.apiKey);
        }
        if (postString is not null)
        {
            request.Content = new StringContent(postString, Encoding.UTF8, "application/json");
        }
        return request;
    }
}
