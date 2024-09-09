using Exchanges.Exceptions;
using Exchanges.Services.Interfaces;
using Exchnages.Utils;
using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Exchanges.Services;

public class ApiService : IApiService
{
    private readonly IConfiguration _configuration;
    public ApiService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<T?> SendSignedGetAsync<T>(
        string requestUrl,
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

        headers.Add(signatureHeaderName, GenerateSignature(queryString, currentTimeStamp));

        requestUrl = queryString.Length > 0 ? requestUrl + "?" + queryString : requestUrl;
        var response = await SendAsync<T>(
            requestUrl: requestUrl,
            httpMethod: HttpMethod.Get,
            headers: headers
            );

        return response;
    }

    public async Task<T?> SendSignedPostAsync<T>(
        string requestUrl,
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
            requestUrl: requestUrl,
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
        var apiKey = _configuration["Mufex:ApiKey"];
        var secretKey = _configuration["Mufex:SecretKey"];
        var recvWindow = _configuration["RecvWindow"];

        string rawData = currentTimeStamp + apiKey + recvWindow + queryString;
        try
        {
            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(secretKey))
                throw new Exception("Please set your api key and api secret");
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
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
       string requestUrl,
       HttpMethod httpMethod,
       Dictionary<string, string> headers,
       string? postString = null
       )
    {
        using var httpClient = new HttpClient();

        HttpRequestMessage request = new(httpMethod, _configuration["Mufex:Mainnet"] + requestUrl);
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
}

