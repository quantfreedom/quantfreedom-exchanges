using Newtonsoft.Json;

namespace mufex.net.api.Models;
public class GeneralResponse<T>
{
    // do I need to have this json property? TODO: check

    [JsonProperty("code")]
    public int code { get; set; }

    [JsonProperty("message")]
    public string message { get; set; }

    [JsonProperty("data")]
    public T? data { get; set; }

    [JsonProperty("ext_info")]
    public Dictionary<string, object>? ext_info { get; set; }

    [JsonProperty("time")]
    public long? time { get; set; }
}
