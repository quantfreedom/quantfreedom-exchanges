using Exchanges.Utils;
using Microsoft.Extensions.Configuration;

namespace Exchanges.Mufex;

public class Mufex : Exchange
{
    private readonly IConfiguration _configuration;

    public Mufex(
        IConfiguration configuration,
    ) : base("mufex url", recvWindow)
    {
        _configuration = configuration;
    }
    protected override Dictionary<string, object> BuildHttpRequest(
            )
    {
        var headers = new Dictionary<string, object>();
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
        return request;
    }
}
