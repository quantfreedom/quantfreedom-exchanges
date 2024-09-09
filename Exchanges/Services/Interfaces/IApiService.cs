namespace Exchanges.Services.Interfaces
{
    public interface IApiService
    {
        Task<T?> SendSignedGetAsync<T>(
            string requestUrl,
            string currentTimeStamp,
            string signatureHeaderName,
            Dictionary<string, string> headers,
            Dictionary<string, object>? query = null);

        Task<T?> SendSignedPostAsync<T>(
            string requestUrl,
            string currentTimeStamp,
            string signatureHeaderName,
            Dictionary<string, string> headers,
            Dictionary<string, object>? query = null
            );
    }
}
