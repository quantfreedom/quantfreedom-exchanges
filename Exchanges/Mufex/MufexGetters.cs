//using Exchanges.Utils;
//using Exchnages.Utils;
//using Newtonsoft.Json;
//using Models.Mufex;

//namespace Exchanges.Mufex;

//public class MufexGetters : Mufex
//{
//    public MufexGetters(
//        string? apiKey = null,
//        string? secretKey = null,
//        string? baseUrl = null,
//        bool debugMode = false,
//        string recvWindow = ExchangeConstants.DEFAULT_REC_WINDOW
//    ) : base(apiKey, secretKey, baseUrl, debugMode, recvWindow)
//    {
//    }

//    public async Task<List<GetWalletBalanceDataList>?> GetAccountBalance(string? coin = null)
//    {
//        var query = new Dictionary<string, object>();

//        ExchangeUtils.AddOptionalParameters(
//            query,
//            ("coin", coin)
//        );

//        var response = await this.SendSignedAsync<string>(
//            requestUrl: "/private/v1/account/balance",
//            httpMethod: HttpMethod.Get,
//            query: query
//            );
//        var generalResponse = JsonConvert.DeserializeObject<GeneralResponse<GetWalletBalanceData>>(response);
//        var responseList = generalResponse.data.list;
//        return responseList;
//    }
//    /// <summary>
//    /// https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-tradingfeerate
//    /// </summary>
//    /// <param name="coin"></param>
//    /// <returns> Account Balance </returns>
//    public async Task<List<TradingFeeDataList>?> GetTradingFeeRates(string? symbol = null)
//    {
//        var query = new Dictionary<string, object>();

//        ExchangeUtils.AddOptionalParameters(
//            query,
//            ("symbol", symbol)
//        );

//        var response = await this.SendSignedAsync<string>(
//            requestUrl: "/private/v1/account/trade-fee",
//            httpMethod: HttpMethod.Get,
//            query: query
//            );
//        var generalResponse = JsonConvert.DeserializeObject<GeneralResponse<TradingFeeData>>(response);
//        var responseList = generalResponse.data.list;
//        return responseList;
//    }
//}
