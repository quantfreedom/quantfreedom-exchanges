using mufex.net.api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace mufex.net.api.Utils;
public class MufexUtils
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

    public static List<WsTradeDataParsed> WsTradeParseModel(WsTradeData tradeData)
    {
        return tradeData.data.tradeList
            .GroupBy(t => new { Price = t[1], Time = t[3], IsBid = t[4] })
            .Select(t =>
            {
                var assetQty = t.Sum(x => double.Parse(x[2]));
                var price = double.Parse(t.Key.Price);
                var timestamp = DateTime.Parse(t.Key.Time);
                var isBuyer = t.Key.IsBid == "b" ? "Buyer" : "Seller";

                return new WsTradeDataParsed
                {
                    Timestamp = timestamp,
                    AssetQty = assetQty,
                    Price = price,
                    IsBuyer = isBuyer,
                    UsdtQty = assetQty * price
                };
            })
            .ToList();
    }
    public static List<CandleDataParsed> GetCandleParseModel(CandleData tradeData)
    {
        return tradeData.List
            .GroupBy(
                t => new
                {
                    Timestamp = t[0],
                    Open = t[1],
                    High = t[2],
                    Low = t[3],
                    Close = t[4],
                    Volume = t[5],
                }
            )
            .Select(
                t =>
                {
                    var timestamp = long.Parse(t.Key.Timestamp);
                    var open = double.Parse(t.Key.Open);
                    var high = double.Parse(t.Key.High);
                    var low = double.Parse(t.Key.Low);
                    var close = double.Parse(t.Key.Close);
                    var volume = double.Parse(t.Key.Volume);

                    return new CandleDataParsed
                    {
                        Timestamp = timestamp,
                        Open = open,
                        High = high,
                        Low = low,
                        Close = close,
                        Volume = volume
                    };
                }
            )
            .ToList();
    }
}
