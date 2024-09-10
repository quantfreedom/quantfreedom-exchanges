using System.Diagnostics.Contracts;

namespace Exchanges.Services.Interfaces;


public interface IApiService
{
    public string ApiKey { get; set; }
    public string SecretKey { get; set; }
    public string BaseUrl { get; set; }

    Task<T?> SendSignedGetAsync<T>(
        string endPoint,
        string currentTimeStamp,
        string signatureHeaderName,
        Dictionary<string, string> headers,
        Dictionary<string, object>? query = null);

    Task<T?> SendSignedPostAsync<T>(
        string endPoint,
        string currentTimeStamp,
        string signatureHeaderName,
        Dictionary<string, string> headers,
        Dictionary<string, object>? query = null
        );
}
