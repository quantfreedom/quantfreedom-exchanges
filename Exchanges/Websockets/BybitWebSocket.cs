using Models.Bybit;
using Models.Exchange;
using Newtonsoft.Json;
using Serilog.Core;

namespace Exchanges.Websockets;


public class BybitWebSocket : ExchangesWebSocket
{
    public BybitWebSocket(Logger logger) : base(logger, BybitConstants.WS_LINEAR_MAINNET) { }

    public override Dictionary<string, object> CreateSubscriptionMessage()
    {

        var subMessage = new Dictionary<string, object>
        {
            {"req_id", Guid.NewGuid().ToString()},
            {"op", "subscribe"},
            {"args", new string[] { "publicTrade.BTCUSDT" }},
        };

        return subMessage;
    }
    public List<ExchangeWsTradeDataParsed> TradeStreamParser(string receivedMessage)
    {
        try
        {
            var bybitTradeData = JsonConvert.DeserializeObject<BybitWsTradeDataList>(receivedMessage);
            var exchangeTradeDataList = bybitTradeData.TradeList
            .GroupBy(t => new { Price = t.Price, Time = t.Timestamp, Side = t.Side })
            .Select(t =>
            {
                var assetQty = t.Sum(x => x.AssetQty);
                var price = t.Key.Price;
                var timestamp = t.Key.Time;
                var side = t.Key.Side;

                return new ExchangeWsTradeDataParsed
                {
                    Exchange = "Bybit",
                    Datetime = DateTimeOffset.FromUnixTimeMilliseconds(timestamp).UtcDateTime.ToString("yyyy-MM-dd HH:mm:ss.ffff"),
                    Timestamp = timestamp,
                    AssetQty = assetQty,
                    Price = price,
                    Side = side,
                    UsdtQty = assetQty * price
                };
            })
            .ToList();
            return exchangeTradeDataList;
        }
        catch (Exception ex)
        {
            this._logger.Error(ex, receivedMessage);
            return new List<ExchangeWsTradeDataParsed>();
        }
    }
}
