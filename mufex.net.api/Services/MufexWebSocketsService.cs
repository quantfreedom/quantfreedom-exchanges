// namespace mufex.net.api.Services;

using System.Net.Http.Headers;
using mufex.net.api.Models;

public class MufexWsService
{

    public static List<WsTradeDataParsed> ParseModel(WsTradeData tradeData)
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

}