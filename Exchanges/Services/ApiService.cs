using Exchanges.Exceptions;
using Exchanges.Services.Interfaces;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Exchanges.Services;

public class ApiService : IApiService
{
    public string ApiKey { get; set; }
    public string SecretKey { get; set; }
    public string BaseUrl { get; set; }
    public async Task<T?> SendSignedGetAsync<T>(
        string endPoint,
        string currentTimeStamp,
        string signatureHeaderName,
        Dictionary<string, string> headers,
        Dictionary<string, object>? query = null
    )
    {
        string queryString = string.Empty;
        if (query is not null)
        {
            queryString = GenerateQueryString(query);
        }

        var signature = GenerateSignature(queryString, currentTimeStamp);
        headers.Add(signatureHeaderName, signature);

        endPoint = queryString.Length > 0 ? endPoint + "?" + queryString : endPoint;
        var response = await SendAsync<T>(
            endPoint: endPoint,
            httpMethod: HttpMethod.Get,
            headers: headers
            );

        return response;
    }

    public async Task<T?> SendSignedPostAsync<T>(
        string endPoint,
        string currentTimeStamp,
        string signatureHeaderName,
        Dictionary<string, string> headers,
        Dictionary<string, object>? query = null
        )
    {
        string queryString = null;

        if (query is not null)
        {
            queryString = JsonConvert.SerializeObject(query);
        }

        headers.Add(signatureHeaderName, GenerateSignature(queryString ?? string.Empty, currentTimeStamp));
        var response = await SendAsync<T>(
            endPoint: endPoint,
            httpMethod: HttpMethod.Post,
            headers: headers,
            postString: queryString
            );

        return response;

    }

    private string GenerateQueryString(IDictionary<string, object> parameters)
    {
        var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={p.Value}"));
        return queryString;
    }

    private string GenerateSignature(string queryString, string currentTimeStamp)
    {
        string rawData = currentTimeStamp + this.ApiKey + "5000" + queryString;
        try
        {
            if (string.IsNullOrEmpty(this.ApiKey) || string.IsNullOrEmpty(this.SecretKey))
                throw new Exception("Please set your api key and api secret");
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(this.SecretKey));
            byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            var signature = BitConverter.ToString(computedHash).Replace("-", "").ToLower();
            return signature;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to calculate HMAC-SHA256 ", ex);
        }
    }

    private async Task<T?> SendAsync<T>(
       string endPoint,
       HttpMethod httpMethod,
       Dictionary<string, string> headers,
       string? postString = null
       )
    {
        using var httpClient = new HttpClient();

        HttpRequestMessage request = new(httpMethod, this.BaseUrl + endPoint);
        foreach (var header in headers)
        {
            request.Headers.Add(header.Key, header.Value);
        }

        if (postString is not null)
        {
            request.Content = new StringContent(postString, Encoding.UTF8, "application/json");
        }

        HttpResponseMessage response = await httpClient.SendAsync(request);

        using HttpContent responseContent = response.Content;
        string contentString = await responseContent.ReadAsStringAsync();

        //TODO clean this part
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
                    var clientException = new ExchangeClientException($"Failed to map server response from '{endPoint}' to given type", -1, ex)
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
}

