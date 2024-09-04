using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace mufex.net.api
{
    public class HmacSignatureGenerator
    {
        public string GenerateSignature(
            string queryString,
            string currentTimeStamp,
            string apiKey,
            string apiSecret,
            string recWindow
        )
        {
            string rawData = currentTimeStamp + apiKey + recWindow + queryString;
            try
            {
                if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
                    throw new Exception("Please set your api key and api secret");
                using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(apiSecret));
                byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                var signature = BitConverter.ToString(computedHash).Replace("-", "").ToLower();
                return signature;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to calculate HMAC-SHA256 ", ex);
            }
        }
    }
}