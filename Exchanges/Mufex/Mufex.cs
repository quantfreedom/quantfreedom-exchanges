using System;
using System.Text;
using Exchanges.Utils;

namespace Exchanges.Mufex;

public class Mufex : Exchange
{
    public Mufex(
        string? apiKey = null,
        string? secretKey = null,
        string? baseUrl = null,
        bool debugMode = false,
        string recvWindow = ExchangeConstants.DEFAULT_REC_WINDOW
    ) : base(apiKey, secretKey, baseUrl, debugMode, recvWindow)
    {
    }
    protected override HttpRequestMessage BuildHttpRequest(
            string requestUrl,
            HttpMethod httpMethod,
            string? signature,
            string? postString
            )
    {
        var theUrl = this.BaseUrl + requestUrl;
        HttpRequestMessage request = new(httpMethod, theUrl);
        request.Headers.Add("User-Agent", this.UserAgent);
        if (signature != null && signature.Length > 0)
        {
            request.Headers.Add("MF-ACCESS-SIGN", signature);
        }
        request.Headers.Add("MF-ACCESS-SIGN-TYPE", ExchangeConstants.DEFAULT_SIGN_TYPE);
        request.Headers.Add("MF-ACCESS-TIMESTAMP", this.CurrentTimeStamp);
        request.Headers.Add("MF-ACCESS-RECV-WINDOW", RecvWindow);
        request.Headers.Add("X-Referer", "AKF3CWKDT");
        if (this.ApiKey is not null)
        {
            request.Headers.Add("MF-ACCESS-API-KEY", this.ApiKey);
        }
        if (postString is not null)
        {
            request.Content = new StringContent(postString, Encoding.UTF8, "application/json");
        }
        return request;
    }
}
