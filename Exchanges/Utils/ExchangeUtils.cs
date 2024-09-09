using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Exchnages.Utils;
public class ExchangeUtils
{

    public static string GetCurrentTimeStampString()
    {
        return GetCurrentTimeStampLong().ToString();
    }
    public static long GetCurrentTimeStampLong()
    {
        long timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        return timestamp;
    }

    public static void AddOptionalParameters(
        Dictionary<string, object> query,
        params (string key, object? value)[] parameters)
    {
        foreach (var (key, value) in parameters)
        {
            if (value != null && !(value is string strValue && string.IsNullOrEmpty(strValue)))
            {
                query.Add(key, value);
            }
        }
    }

    public static long? GetUnixTimeStampLong(DateTime? dateTime)
    {
        if (dateTime == null)
        {
            return null;
        }
        else
        {
            DateTime utcTime = DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc);
            long unixTimeStamp = ((DateTimeOffset)utcTime).ToUnixTimeSeconds();
            return unixTimeStamp;
        }
    }
    public static long GetUnixTimeStampString(DateTime dateTime)
    {
        long unixTimeStamp = ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
        return unixTimeStamp;
    }

    public static void PrintJsonResult(object jsonResult)
    {
        string jsonResultString = JsonConvert.SerializeObject(jsonResult);
        string jsonFormatted = JValue.Parse(jsonResultString).ToString(Formatting.Indented);
        Console.WriteLine(jsonFormatted);
    }

}
