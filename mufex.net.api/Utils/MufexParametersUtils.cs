using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace mufex.net.api
{
    public class MufexParametersUtils
    {

        /// <summary>
        /// Get current time stamp
        /// </summary>
        /// <returns>timestamp in mille seconds</returns>
        public static string GetCurrentTimeStampString()
        {
            return GetCurrentTimeStampLong().ToString();
        }
        public static long GetCurrentTimeStampLong()
        {
            long timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            return timestamp;
        }

        /// <summary>
        /// Adds optional parameters to the provided query dictionary if they have valid values.
        /// </summary>
        /// <param name="query">The dictionary to which the optional parameters will be added.</param>
        /// <param name="parameters">An array of key-value pairs representing optional parameters.</param>

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
}
